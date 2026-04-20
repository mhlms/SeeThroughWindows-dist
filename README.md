# SeeThroughWindows 🪟✨

> 按下全局热键，让任意窗口瞬间透明！

[![GitHub release](https://img.shields.io/github/v/release/mhlms/SeeThroughWindows-dist?style=flat-square)](https://github.com/mhlms/SeeThroughWindows-dist/releases)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg?style=flat-square)](LICENSE)
[![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square)](https://dotnet.microsoft.com/)

---

## 📖 软件简介

**SeeThroughWindows** 是一款轻量级的 Windows 窗口透明化工具。通过自定义全局热键，您可以随时切换当前窗口的透明度、开启鼠标穿透、置顶窗口，甚至一键将窗口移动到其他显示器。

本仓库是 `SeeThroughWindows` 的一个分支，在原有强大功能的基础上，增加了**完整的多语言界面支持**，并对构建流程进行了优化。

---

## ✨ 主要特性

### 🎨 现代主题外观 (Catppuccin)

- 内置四种精美主题：**Latte**（亮色）、**Frappé**、**Macchiato**、**Mocha**（暗色）
- 提供 **10 种强调色** 自由搭配（薰衣草紫、蓝、紫红、粉、青绿、绿、桃、黄、红、天蓝）
- 所有主题均可实时切换，无需重启软件

### 🌐 多语言界面（本分支新增）

- 软件界面现已支持多种语言显示，并可根据需要轻松扩展
- 语言设置自动保存，下次启动时自动恢复
- 开发者可极简地追加新语言，无需修改业务代码

### ⚡ 核心窗口操作

- **全局热键** – 可自定义组合键（如 `Ctrl+Win+T`）快速切换窗口透明状态
- **鼠标穿透** – 让透明窗口忽略鼠标点击，方便透过窗口操作下层内容
- **窗口置顶** – 配合透明效果，让目标窗口始终浮在最前
- **多显示器支持** – 使用热键将窗口快速移动到其他屏幕
- **透明度微调** – 通过热键逐步增加/减少透明度
- **自动应用** – 可选择在软件启动时自动将透明效果应用到所有可见窗口
- **全局重置** – 一键恢复所有被透明化的窗口

### 🔧 开发者友好

- 基于服务的模块化架构，代码清晰易于维护
- 内置 `WindowDebugger` 调试工具，方便排查窗口操作问题
- 支持通过 GitHub Actions 自动构建和发布

---

## 📥 下载与安装

### 方式一：直接下载（推荐）

前往 [Releases 页面](https://github.com/mhlms/SeeThroughWindows-dist/releases) 下载最新的发布包。

- **自包含版本** (`*-self-contained-win-x64.zip`)：已内置 .NET 9 运行时，解压即用。
- **框架依赖版本** (`*-framework-dependent.zip`)：体积较小，需自行安装 [.NET 9 桌面运行时](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)。

### 方式二：通过 Scoop 安装（上游原版）

```powershell
scoop bucket add neontowel https://github.com/NeonTowel/scoop-bucket
scoop install neontowel/seethroughwindows
```

> **注意**：Scoop 安装的是上游稳定版，可能不包含本分支的最新多语言功能。

---

## 🚀 使用方法

1. 运行 `SeeThroughWindows.exe`，软件将最小化到系统托盘。
2. 右键点击托盘图标，选择 **“选项”** 打开设置界面。
3. 在 **“外观”** 分组中选择您喜欢的主题、强调色以及界面语言。
4. 在 **“窗口透明化”** 分组中设置您的专属热键（默认 `Ctrl+Win+Z`）。
5. 激活任意窗口，按下热键即可切换透明状态。

### ⌨️ 默认快捷键参考

| 功能 | 快捷键 |
|------|--------|
| 切换透明 | `Ctrl + Win + Z`（可自定义） |
| 最大化/最小化窗口 | `Ctrl + Win + ↑` / `↓` |
| 移动到其他显示器 | `Ctrl + Win + ←` / `→` |
| 增加/减少透明度 | `Ctrl + Win + PageUp` / `PageDown` |

### 💡 温馨提示：误操作鼠标穿透怎么办？

如果您不小心对 **SeeThroughWindows 自己的设置窗口** 启用了“鼠标穿透”，会导致无法用鼠标点击界面。此时请按以下步骤恢复：

1. 按下 **`Win + Tab`** 打开任务视图。
2. 用键盘方向键选中 `SeeThroughWindows` 窗口，按回车激活。
3. 直接按下您设置的热键（例如 `Ctrl+Win+Z`）即可关闭透明/穿透效果，窗口恢复正常。

> 软件已内置防护逻辑，通常情况下**不会**对自身启用鼠标穿透。以上操作仅作为紧急备用。

---

## 🙏 致谢与上游项目

本软件基于以下优秀项目演进而来，在此表示诚挚感谢：

- **[MOBZystems / SeeThroughWindows](https://github.com/mobzystems/SeeThroughWindows)**  
  软件的原始作者，提供了窗口透明化的核心实现与 Win32 交互逻辑。

- **[NeonTowel / SeeThroughWindows-dist](https://github.com/NeonTowel/SeeThroughWindows-dist)**  
  引入了现代化的 Catppuccin 主题系统、服务架构重构、自动应用透明等大量增强功能，并建立了完善的 GitHub Actions 发布流程。

### 🌟 本仓库 (`mhlms/SeeThroughWindows-dist`) 的主要贡献

在 NeonTowel 版本的基础上，新增了以下功能与改进：

- ✅ **完整的界面多语言支持**（语言切换逻辑与持久化存储）
- ✅ **优化的语言扩展机制**（新增语言无需修改全部资源文件）
- ✅ **修复若干构建脚本兼容性问题**
- ✅ **改进用户体验细节**（如防止对自身误启用鼠标穿透）

---

## 🛠️ 开发相关

### 环境要求

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Windows 操作系统（本软件依赖 Win32 API）

### 构建命令

```bash
git clone https://github.com/mhlms/SeeThroughWindows-dist.git
cd SeeThroughWindows-dist
dotnet restore
dotnet build
dotnet run --project SeeThroughWindows
```

### 添加新语言

1. 复制 `SeeThroughWindows/Resources/Strings.resx` 并重命名为目标文化名称（如 `Strings.ja.resx`）。
2. 翻译文件中的 `<value>` 内容。
3. 在 `SeeThroughWindows/Services/LocalizationService.cs` 的 `supportedLanguages` 数组中添加一行新语言条目。
4. 重新编译即可。

详细开发文档请参考项目中的 `docs/DEVELOPMENT.md` 及各功能模块的注释。

---

## 📄 许可证

本项目使用 **GNU General Public License v3.0 (GPL-3.0)** 许可证。  
这意味着您可以自由使用、修改和分发本软件，但衍生作品也必须以相同的许可证开源。

完整许可证文本请参阅仓库中的 [LICENSE](LICENSE) 文件。

---

## 🔗 相关链接

- 🏠 **本仓库**：[mhlms/SeeThroughWindows-dist](https://github.com/mhlms/SeeThroughWindows-dist)
- 🌟 **上游仓库 (NeonTowel)**：[NeonTowel/SeeThroughWindows-dist](https://github.com/NeonTowel/SeeThroughWindows-dist)
- 📜 **原始项目 (MOBZystems)**：[mobzystems/SeeThroughWindows](https://github.com/mobzystems/SeeThroughWindows)
- 🎨 **Catppuccin 配色**：[https://catppuccin.com](https://catppuccin.com)

---

*Made with 💜 by the SeeThroughWindows community*
