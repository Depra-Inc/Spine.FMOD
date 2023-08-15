# Depra.Spine.FMOD

<div align="center">
    <strong><a href="README.md">English</a> | <a href="README.RU.md">Русский</a></strong>
</div>

<details>
<summary>Содержание</summary>

- [Введение](#введение)
    - [Необходимые компоненты](#необходимые-компоненты)
    - [Ознакомьтесь с этим](#ознакомьтесь-с-этим)
- [Особенности](#особенности)
- [Интеграция](#интеграция)
- [Зависимости](#зависимости)
- [Поддержка](#поддержка)
- [Лицензия](#лицензия)

</details>

## Введение

Модуль позволяет синхронизировать события **Spine** с событиями **FMOD**.<br>

### Необходимые компоненты

Для использования `Depra.Spine.FMOD` вам понадобятся следующие компоненты:

- [Spine Runtime для Unity](http://it.esotericsoftware.com/spine-unity-download)
- [Пакет интеграции FMOD-Unity](https://www.fmod.com/unity)

### Ознакомьтесь с этим

- [Тема на форуме Spine](http://esotericsoftware.com/forum/Free-FMOD-Wwise-Audio-Integration-Tool-14845)<br>
- [Страница FMOD на Unity Asset Store](https://assetstore.unity.com/packages/tools/audio/spine2fmod-181263)<br>

## Особенности

- Гибкая настройка;
- Поддержка нескольких типов синхронизации:

| **Spine** \ **FMOD** | `EventReference`                   | `StudioEventEmitter`                |
|----------------------|------------------------------------|-------------------------------------|
| Начало анимации      | ✅ `BindSpineAnimationToFMODEvents` | ✅ `BindSpineAnimationToFMODEmitter` |
| Событие анимации     | ✅ `BindSpineEventsToFMODEvents`    | ✅ `BindSpineEventsToFMODEmitter`    |

- Поддержка расширений для `EventInstance`:
    - `FMODEventLogging` - Вывод имени события в консоль.
    - `FMODEventCallbacks` - Добавляет обратных вызовов для события.
    - `FMODEventFollowingTransform` - Добавляет следование позиции звука за `UnityEngine.Transform`.
    - `FMODEventFollowingRigidbody` - Добавляет следование позиции звука за `UnityEngine.Rigidbody`.
    - `FMODEventFollowingRigidbody2D` - Добавляет следование позиции звука за `UnityEngine.Rigidbody2D`.

## Интеграция

1. Скачайте и интегрируйте последнюю
   версию [Spine Runtime для Unity](http://it.esotericsoftware.com/spine-unity-download);
2. Скачайте и интегрируйте последнюю версию [Пакета интеграции FMOD-Unity](https://www.fmod.com/unity);
3. Добавьте события в вашем проекте **Spine**:

<p>
<img src="https://i.ibb.co/3ycvLRK/spine-event.png" alt="События Spine">
</p>

4. Убедитесь, что события включены в экспорте анимации Spine в Unity:

<p>
<img src="https://i.ibb.co/B3YRpxW/unity-animation-preview.png" alt="Экспорт событий Spine">
</p>

5. Добавьте один из скриптов привязки как компонент к объекту **Spine** и используйте размер для выбора количества
   анимаций.
6. Выберите событие/анимацию **Spine** и событие/эмиттер **FMOD**, которые вы хотите синхронизировать.
7. При необходимости добавьте расширения события FMOD.

## Зависимости

| Зависимость | Поддерживаемые версии |
|-------------|-----------------------|
| Unity       | 2018.4 и выше         |
| Spine Unity | 4.1 (2023-06-27)      |
| FMOD Studio | 2.02.15               |

## Поддержка

Я независимый разработчик,
и большая часть работы над этим проектом выполняется в свободное время.
Если вас интересует сотрудничество или вы хотите нанять меня для проекта,
ознакомьтесь с [моим портфолио](https://github.com/Depression-aggression) и [свяжитесь](mailto:g0dzZz1lla@yandex.ru) со
мной!

## Лицензия

**Apache-2.0**

Авторские права (c) 2022-2023 Николай Мельников
[g0dzZz1lla@yandex.ru](mailto:g0dzZz1lla@yandex.ru)