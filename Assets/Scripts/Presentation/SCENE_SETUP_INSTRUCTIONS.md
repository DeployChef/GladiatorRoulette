# Инструкция по настройке сцены для Рулетки Гладиаторов

## Шаг 1: Создание ScriptableObjects для гладиаторов

### 1.1 Создание GladiatorData (данные одного гладиатора)

1. В Project окне: ПКМ → `Create → GladiatorRoulette → Gladiator Data`
2. Назовите его, например: `Gladiator_Spartacus`
3. В Inspector установите:
   - **Gladiator Name**: "Спартак" (или любое имя)
   - **Color**: Выберите цвет для этого гладиатора (например, красный)

4. Повторите для всех 8 гладиаторов:
   - `Gladiator_Spartacus`
   - `Gladiator_Maximus`
   - `Gladiator_Commodus`
   - `Gladiator_Crixus`
   - `Gladiator_Gannicus`
   - `Gladiator_Oenomaus`
   - `Gladiator_Agron`
   - `Gladiator_Castus`

### 1.2 Создание GladiatorDatabase (база данных)

1. В Project окне: ПКМ → `Create → GladiatorRoulette → Gladiator Database`
2. Назовите: `GladiatorDatabase`
3. В Inspector в списке **All Gladiators** добавьте все 8 созданных `GladiatorData`

## Шаг 2: Настройка Canvas для UI

1. Создайте Canvas: `GameObject → UI → Canvas`
2. Настройте Canvas:
   - **Render Mode**: Screen Space - Overlay (по умолчанию)
   - **Canvas Scaler**: Scale With Screen Size
   - **Reference Resolution**: 1920x1080

## Шаг 3: Создание UI элементов

### 3.1 Кнопка "В бой"

1. ПКМ на Canvas → `UI → Button - TextMeshPro`
2. Назовите: `FightButton`
3. Настройте:
   - **Text**: "В БОЙ!"
   - Позиция: внизу по центру (например, Y = -400)
   - Размер: примерно 200x60

### 3.2 Текст победителя

1. ПКМ на Canvas → `UI → Text - TextMeshPro`
2. Назовите: `WinnerText`
3. Настройте:
   - **Text**: "" (пусто)
   - Позиция: вверху по центру (например, Y = 400)
   - Размер шрифта: 48
   - Выравнивание: по центру
   - Цвет: желтый или золотой

4. (Опционально) Создайте панель для победителя:
   - ПКМ на Canvas → `UI → Panel`
   - Назовите: `WinnerPanel`
   - Добавьте `WinnerText` как дочерний объект
   - Настройте цвет фона (полупрозрачный)

## Шаг 4: Создание визуальных элементов боя

### 4.1 Контейнер для гладиаторов

1. Создайте пустой GameObject: `GameObject → Create Empty`
2. Назовите: `GladiatorsContainer`
3. Добавьте компонент `RectTransform` (если его нет):
   - ПКМ на объекте → `Add Component → Rect Transform`
4. Настройте:
   - **Anchor**: Center
   - **Position**: (0, 0, 0)
   - **Width/Height**: 800x800 (или по размеру арены)

### 4.2 Префаб GladiatorView

1. Создайте пустой GameObject как дочерний для `GladiatorsContainer`
2. Назовите: `GladiatorView_Prefab`
3. Добавьте компоненты:
   - `RectTransform`
   - `Image` (UI → Image)
   - `TextMeshProUGUI` (UI → Text - TextMeshPro)
   - `GladiatorView` (скрипт)

4. Настройте структуру:
   ```
   GladiatorView_Prefab
   ├── RectTransform
   ├── Image (gladiatorRect) - прямоугольник гладиатора
   │   └── Size: 80x80
   │   └── Color: любой
   └── TextMeshProUGUI (nameText) - имя гладиатора
       └── Position: под прямоугольником (Y = -50)
       └── Font Size: 24
       └── Alignment: Center
   ```

5. В Inspector на компоненте `GladiatorView`:
   - Перетащите `Image` в поле **Gladiator Rect**
   - Перетащите `TextMeshProUGUI` в поле **Name Text**
   - **Jump Height**: 50
   - **Jump Duration**: 0.5

6. Сделайте префаб:
   - Перетащите `GladiatorView_Prefab` в папку `Assets` (создастся префаб)
   - Удалите объект со сцены (префаб останется)

### 4.3 Дым (Smoke Effect)

1. ПКМ на Canvas → `UI → Image`
2. Назовите: `SmokeEffect`
3. Настройте:
   - **Position**: (0, 0, 0) - центр экрана
   - **Size**: 600x600 (или больше, чтобы покрыть арену)
   - **Color**: Серый, полупрозрачный (R:128, G:128, B:128, A:180)
   - **Image Type**: Simple
   - По умолчанию должен быть **неактивен** (галочка в Inspector)

## Шаг 5: Создание главного объекта GameBootstrap

1. Создайте пустой GameObject: `GameObject → Create Empty`
2. Назовите: `GameBootstrap`
3. Добавьте компонент `GameBootstrap` (скрипт)

4. В Inspector настройте `GameBootstrap`:
   - **Gladiator Database**: Перетащите созданный `GladiatorDatabase`
   - **Fight View**: (настроим в следующем шаге)
   - **Fight UI Controller**: (настроим в следующем шаге)
   - **Gladiators Per Fight**: 8

## Шаг 6: Настройка FightAnimationPlayer

1. Создайте пустой GameObject: `GameObject → Create Empty`
2. Назовите: `FightAnimationPlayer`
3. Добавьте компонент `FightAnimationPlayer` (скрипт)

4. В Inspector настройте `FightAnimationPlayer`:
   - **Jump Delay**: 0.1
   - **Smoke Appear Duration**: 0.5
   - **Smoke Fight Duration**: 2.0
   - **Elimination Delay**: 0.3
   - **Gladiator Views**: Оставьте пустым (заполнится автоматически при инициализации)

## Шаг 7: Настройка FightView

1. Создайте пустой GameObject: `GameObject → Create Empty`
2. Назовите: `FightView`
3. Добавьте компонент `FightView` (скрипт)

4. В Inspector настройте `FightView`:
   - **Gladiator View Prefab**: Перетащите созданный префаб `GladiatorView_Prefab`
   - **Gladiators Container**: Перетащите `GladiatorsContainer`
   - **Arena Radius**: 300 (радиус круга для размещения гладиаторов)
   - **Arena Center**: (0, 0) - центр
   - **Animation Player**: Перетащите `FightAnimationPlayer` (ВАЖНО!)
   - **Smoke Effect**: Перетащите `SmokeEffect`

5. Вернитесь в `GameBootstrap` и перетащите `FightView` в поле **Fight View**

## Шаг 8: Настройка FightUIController

1. Создайте пустой GameObject: `GameObject → Create Empty`
2. Назовите: `FightUIController`
3. Добавьте компонент `FightUIController` (скрипт)

4. В Inspector настройте `FightUIController`:
   - **Fight Button**: Перетащите `FightButton`
   - **Fight Button Text**: Перетащите Text компонент кнопки (обычно дочерний объект)
   - **Winner Text**: Перетащите `WinnerText`
   - **Winner Panel**: (опционально) Перетащите `WinnerPanel`, если создали

5. Вернитесь в `GameBootstrap` и перетащите `FightUIController` в поле **Fight UI Controller**

## Шаг 9: Проверка и запуск

1. Убедитесь, что все ссылки в Inspector заполнены
2. Сохраните сцену: `File → Save`
3. Нажмите Play
4. Нажмите кнопку "В БОЙ!" и наблюдайте за анимацией!

## Структура сцены (итоговая)

```
Scene
├── Canvas
│   ├── FightButton
│   │   └── Text (TMP)
│   ├── WinnerText (или WinnerPanel → WinnerText)
│   └── SmokeEffect (неактивен по умолчанию)
├── GladiatorsContainer (RectTransform)
├── GameBootstrap (GameBootstrap script)
├── FightView (FightView script)
│   └── ссылка на FightAnimationPlayer
├── FightAnimationPlayer (FightAnimationPlayer script) - ВАЖНО!
└── FightUIController (FightUIController script)
```

## Важные замечания

- **DOTween**: Убедитесь, что DOTween установлен в проекте. Если нет, установите через Package Manager или Asset Store.
- **TextMeshPro**: Если при создании UI элементов появляется окно импорта TextMeshPro, нажмите "Import TMP Essentials".
- **Порядок инициализации**: `GameBootstrap` в `Awake` создает все зависимости, поэтому порядок объектов на сцене не важен.
- **Цвета гладиаторов**: Используйте разные яркие цвета для каждого гладиатора, чтобы их было легко различить.

## Возможные проблемы и решения

1. **Гладиаторы не появляются**: Проверьте, что `GladiatorDatabase` заполнен и назначен в `GameBootstrap`
2. **Кнопка не работает**: Проверьте, что все ссылки в `FightUIController` заполнены
3. **Анимации не работают**: 
   - Убедитесь, что DOTween установлен и инициализирован (обычно автоматически)
   - **ВАЖНО**: Проверьте, что `FightAnimationPlayer` назначен в `FightView` в поле **Animation Player**
4. **Гладиаторы не по кругу**: Проверьте `Arena Radius` и `Arena Center` в `FightView`
5. **Все происходит сразу, нет очереди анимаций**: Убедитесь, что `FightAnimationPlayer` создан и назначен в `FightView`

