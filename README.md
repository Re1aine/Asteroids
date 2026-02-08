# Asteroids
A classic arcade-style Asteroids game built in Unity.

## 🕹️ Controls
| Action | Key |
|--------|-----|
| **Shoot Bullet** | `SPACE` (Press) |
| **Shoot Laser** | `SHIFT` (Hold) |
| **Move Forward** | `W` / `↑` |
| **Rotate Left** | `A` / `←` |
| **Rotate Right** | `D` / `→` |

## 📥 Setup
1. Install `Unity 6000.0.64f1`  
2. Open `Project` scene
3. Play

## 📁 Project Structure
Assets/_Project/Code/
- ├── EntryPoints/          # Bootstrappers & initialization
- ├── GameFlow/             # State machines & states
- ├── Infrastructure/       # Services
- ├── Input/                # Input handling logic
- ├── Logic/                # Core game mechanics
- ├── Tools/                # Editor utilities & debug tools
- ├── Scopes/               # Lifetime scopes
- └── UI/                   # MVP UI implementation

## ✅ Implemented Features

### 🎮 **Core Gameplay**
- **Character Movement** – smooth controls with physics/animation integration  
- **Dual Shooting System** – laser and bullet attacks with distinct mechanics and visuals  
- **Combat System** – hit detection, damage handling, and visual/audio feedback
- **Audio System** – sfx for shooting, explosions, UI
- **VFX System** – vfx for explosions, visual feedback for actions

### ⚙️ **Services & Integrations**
- **Remote Config Service** – dynamic balance tuning and game settings without app updates  
- **Ad Service** – integrated Interstitial, Rewarded ad units
- **IAP Service** – in‑app purchases with receipt validation and platform support  
- **Analytics Service** – custom event tracking, player metrics, and data pipelines  
- **Remote Addressables** - remote content delivery, live asset updates, and versioned bundles without rebuilding the app

## 🛠️ Technology Stack
- **R3** for reactive streams and events
- **Addressables** for assets management
- **UniTask** for modern async/await
- **VContainer** for inject dependencies
- **GamePush SDK** for cloud saves, player auth, for configs, assets etc.
- **LitMotion** for tweening UI and gameplay animations

# Gameplay
![Gameplay](https://github.com/user-attachments/assets/b40d5423-daac-4d10-a7d0-30596022fadc)
