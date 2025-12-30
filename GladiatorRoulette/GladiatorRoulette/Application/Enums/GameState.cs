namespace GladiatorRoulette.Application.Enums;

public enum GameState
{
    Idle,       // Ожидание действий игрока
    Fighting,   // Бой в процессе (анимация)
    Resolving,  // Определение победителя (мгновенно)
    Result      // Показан результат
}