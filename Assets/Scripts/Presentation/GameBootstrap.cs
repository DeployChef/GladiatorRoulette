using Application;
using Application.Battle;
using Application.Enums;
using Domain;
using Domain.Events;
using Domain.Rules;
using Infrastructure;
using Infrastructure.Random;
using Presentation.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Presentation
{
    /// <summary>
    /// Composition Root - точка входа и инициализации всей системы.
    /// Создает все зависимости, связывает компоненты, загружает данные из ScriptableObject.
    /// Это паттерн Dependency Injection через конструктор/методы инициализации.
    /// </summary>
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private GladiatorDatabase gladiatorDatabase; // База данных гладиаторов из редактора
        
        [SerializeField] private BattleSceneController battleSceneController;

        // Зависимости домена и приложения
        private IEventBus _eventBus;
        private IRandomProvider _randomProvider;
        private IFightResolver _fightResolver;
        private Fight _fight;
        private GameStateMachine _gameStateMachine;
        private StartFightUseCase _startFightUseCase;

        // Application layer
        private BattleEventQueue _battleEventQueue;
        private BattleFacade _battleFacade;

        // Списки гладиаторов
        private List<Gladiator> _domainGladiators = new();
        private Dictionary<Gladiator, Color> _gladiatorColors = new Dictionary<Gladiator, Color>();
        
        private void Awake()
        {
            // Инициализируем систему в Awake, чтобы все было готово к Start
            InitializeSystem();
        }
        
        /// <summary>
        /// Инициализация всей системы.
        /// Создает зависимости, загружает данные, связывает компоненты.
        /// </summary>
        private void InitializeSystem()
        {
            // 1. Создаем инфраструктурные зависимости
            _eventBus = new EventBus();
            _randomProvider = new SystemRandomProvider();
            
            // 2. Создаем правила боя
            _fightResolver = new FightRules(_randomProvider);
            
            // 3. Загружаем гладиаторов из ScriptableObject
            LoadGladiatorsFromDatabase();
            
            // 4. Создаем доменную модель боя
            _fight = new Fight(_domainGladiators, _fightResolver, _eventBus);
            
            // 5. Создаем GameStateMachine с Unity-специфичной задержкой через Coroutine
            _gameStateMachine = new GameStateMachine(_eventBus);
            
            // 6. Создаем Use Case для начала боя
            _startFightUseCase = new StartFightUseCase(_fight);

            // 7. Application слой
            _battleEventQueue = new BattleEventQueue();
            _battleFacade = new BattleFacade(_startFightUseCase, _battleEventQueue);

            _eventBus.Subscribe<FightStarted>(OnDomainEvent);
            _eventBus.Subscribe<FightFinished>(OnDomainEvent);

            battleSceneController.Init(_battleFacade, gladiatorDatabase);
        }

        private void OnDomainEvent(IDomainEvent domainEvent)
        {
            _battleFacade.OnDomainEvent(domainEvent);
        }

        /// <summary>
        /// Загружает гладиаторов из GladiatorDatabase.
        /// Создает Domain.Gladiator объекты из GladiatorData.
        /// </summary>
        private void LoadGladiatorsFromDatabase()
        {
            if (gladiatorDatabase == null)
            {
                Debug.LogError("GameBootstrap: GladiatorDatabase not assigned!");
                return;
            }
            
            var gladiatorDataList = gladiatorDatabase.AllGladiators;
            
            if (gladiatorDataList.Count == 0)
            {
                Debug.LogError("GameBootstrap: No gladiators loaded from database!");
                return;
            }
            
            _domainGladiators.Clear();
            
            foreach (var data in gladiatorDataList)
            {
                var gladiator = new Gladiator(data.GetId(), data.Name);
                _domainGladiators.Add(gladiator);
            }
            
            Debug.Log($"GameBootstrap: Loaded {_domainGladiators.Count} gladiators for fight.");
        }
    }
}

