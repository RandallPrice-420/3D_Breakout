<h1>3D_Breakout</h1>

3D_Breakout game using Unity 2022.3.35f1.

Based on the excellent tutorial by Imphenzia.
<![CDATA[<p align="center">
  <img src="Docs/banner.png" alt="AI Workbench Banner" width="800"/>
</p>

<h1 align="center">Complete AI Workbench for Unity</h1>

<p align="center">
  <strong>An Anthropic Claude-powered editor toolkit for AI-assisted game development</strong>
</p>

<p align="center">
  <a href="#features">Features</a> •
  <a href="#requirements">Requirements</a> •
  <a href="#installation">Installation</a> •
  <a href="#quick-start">Quick Start</a> •
  <a href="#editor-windows">Editor Windows</a> •
  <a href="#configuration">Configuration</a> •
  <a href="#schemas--data-pipelines">Schemas</a> •
  <a href="#extending-the-workbench">Extending</a> •
  <a href="#license">License</a>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Unity-2021.3%20LTS%2B-blue?logo=unity" alt="Unity 2021.3+"/>
  <img src="https://img.shields.io/badge/API-Anthropic%20Claude-blueviolet" alt="Anthropic Claude"/>
  <img src="https://img.shields.io/badge/version-1.0.0-green" alt="Version 1.0.0"/>
  <img src="https://img.shields.io/badge/license-MIT-orange" alt="MIT License"/>
</p>

---

## Overview

The **Complete AI Workbench** is a modular Unity editor toolkit that integrates Anthropic's Claude API directly into the Unity Editor. It provides AI-assisted workflows for level design, narrative scripting, quest authoring, loot balancing, asset metadata generation, and more — all without leaving the editor.

Design a quest by describing it in plain language. Generate loot tables with balanced rarity curves. Build node-based level layouts with AI suggestions. The workbench handles the Claude API communication, prompt management, and structured data export so you can focus on creative decisions.

---

## Features

| Category | Capabilities |
|----------|-------------|
| **Level Design** | Visual node graph editor, AI-generated layouts, prefab assembly with AI suggestions |
| **Narrative** | Timeline-based cutscene sequencer, AI dialogue generation, multi-track editing |
| **Quests** | Branching quest graph editor, linear quest flow editor, conditional logic builder |
| **Economy** | Loot rarity balancer, distribution curve editor, drop rate simulation |
| **Asset Management** | Batch AI metadata generation, custom tagging schemas, folder-level processing |
| **Developer Tools** | Prompt debugger, token counter, cost estimator, conversation simulator |
| **Infrastructure** | Secure API key management, async request pipeline, retry with backoff, streaming |

---

## Requirements

- **Unity** 2021.3 LTS or later (2022.3 LTS recommended)
- **Anthropic API Key** — obtain from [console.anthropic.com](https://console.anthropic.com)
- **.NET** Standard 2.1 / .NET Framework 4.x scripting backend
- **Newtonsoft JSON** (com.unity.nuget.newtonsoft-json) — auto-resolved via package manifest

---

## Installation

### Option A: Unity Package Manager (recommended)

1. Open **Window > Package Manager**
2. Click **+** → **Add package from git URL...**
3. Enter:
   ```
   https://github.com/your-org/ai-workbench.git
   ```
4. Click **Add** and wait for import to complete

### Option B: Import .unitypackage

1. Download the latest `AIWorkbench_v1.0.0.unitypackage` from [Releases](https://github.com/your-org/ai-workbench/releases)
2. In Unity: **Assets > Import Package > Custom Package...**
3. Select the downloaded file and import all assets

### Option C: Manual Installation

1. Clone or download this repository
2. Copy the `AIWorkbench/` folder into your project's `Assets/` directory
3. Let Unity recompile — no additional setup needed

---

## Quick Start

### 1. Run the Bootstrap Wizard

On first import, the **Bootstrap Wizard** launches automatically.  
If it doesn't, open it via **Window > AI Workbench > Setup Wizard**.

| Step | Action |
|------|--------|
| **API Key** | Enter your Anthropic API key (`sk-ant-...`) |
| **Model** | Select your default Claude model (e.g., `claude-3.5-sonnet`) |
| **Folders** | Let the wizard scaffold the recommended folder structure |
| **Prompts** | Choose the built-in prompt library or import your own JSON |
| **Verify** | The wizard sends a test prompt to confirm connectivity |

### 2. Open the Toolkit Hub

Navigate to **Window > AI Workbench > Toolkit Hub** to access all editor windows from one dashboard. The hub displays:

- API connection status and active model
- Token usage for the current session
- Quick-launch buttons for every specialized window
- Recent activity log

### 3. Start Creating

Pick any editor window and start generating. For example:
- Open **Level Node Graph** → type "underground dungeon with 8 rooms, a boss arena, and two secret passages" → click Generate
- Open **Quest Flow Editor** → type "escort mission with ambush halfway through" → click Generate

---

## Editor Windows

### AnthropicToolkitWindow — *Toolkit Hub*
> **Window > AI Workbench > Toolkit Hub**

Central dashboard for API status, token tracking, window launching, and batch operations.

### LevelNodeGraphWindow — *Level Node Graph*
> **Window > AI Workbench > Level Node Graph**

Visual node-based graph editor for designing level layouts. Supports AI-generated room graphs with typed nodes (Room, Corridor, Arena, Spawn, Exit, Transition, Event Trigger), edge properties, validation, and export to `LevelGraphSchema` JSON.

### LevelPrefabBuilderWindow — *Prefab Builder*
> **Window > AI Workbench > Prefab Builder**

Drag-and-drop level assembly from AI-suggested prefab placements. Reads from Level Node Graph output, supports constraint rules, and batch-instantiates to scene.

### CutsceneTimelineWindow — *Cutscene Timeline*
> **Window > AI Workbench > Cutscene Timeline**

Multi-track timeline editor (Dialogue, Camera, Animation, SFX, Lighting) with AI dialogue generation. Provide characters and scene context — Claude writes the lines. Exports to `CutsceneSchema` JSON or Unity Timeline assets.

### QuestGraphWindow — *Quest Graph*
> **Window > AI Workbench > Quest Graph**

Directed-graph editor for quest dependency chains and branching narratives. Node types include QuestStart, Objective, Branch, Merge, QuestEnd, and FailState. Supports conditional edges, reward assignment, and validation. Exports to `QuestGraphSchema` JSON.

### QuestFlowEditorWindow — *Quest Flow Editor*
> **Window > AI Workbench > Quest Flow Editor**

Simplified linear quest designer for sequential objective chains. Each step includes objective text, completion criteria (Kill, Collect, Deliver, Talk, Reach, Interact, Custom), optional dialogue, and rewards. Exports to `QuestFlowSchema` JSON.

### LootRarityBalancerWindow — *Loot Balancer*
> **Window > AI Workbench > Loot Balancer**

AI-assisted loot table editor with customizable rarity tiers (Common → Legendary), visual distribution curves, per-enemy/per-zone overrides, and a simulation mode for statistical validation. Exports to `LootTableSchema` JSON.

### AssetMetadataGeneratorWindow — *Asset Metadata Generator*
> **Window > AI Workbench > Asset Metadata Generator**

Batch AI-powered metadata generation for Textures, Models, Prefabs, Audio, ScriptableObjects, and Materials. Generates display names, descriptions, tags, categories, and keywords. Supports custom schemas and JSON sidecar export.

### AnthropicPromptDebuggerWindow — *Prompt Debugger*
> **Window > AI Workbench > Prompt Debugger**

Developer-focused prompt testing environment with system prompt editing, multi-turn conversation simulation, real-time token/cost estimation, parameter sliders, diff views, and session export.

---

## Configuration

### AnthropicConfigSO

A `ScriptableObject` asset storing API settings:

| Field | Type | Description |
|-------|------|-------------|
| `ApiEndpoint` | `string` | Anthropic Messages API URL |
| `ModelId` | `string` | Model identifier (e.g., `claude-3-5-sonnet-20241022`) |
| `Temperature` | `float` | Sampling temperature `0.0` – `1.0` |
| `MaxTokens` | `int` | Maximum response tokens |
| `TopP` | `float` | Nucleus sampling parameter |
| `TopK` | `int` | Top-K sampling parameter |

Create multiple config profiles (e.g., `HighQuality`, `FastDraft`, `CodeGen`) and switch between them in the Toolkit Hub.

### PromptLibrarySO

A `ScriptableObject` containing reusable prompt templates:

```
PromptKey:      "level_dungeon_generate"
SystemPrompt:   "You are a game level designer..."
UserTemplate:   "Generate a dungeon layout with {{room_count}} rooms and {{theme}} theme."
Tags:           ["level", "dungeon", "generation"]
Category:       LevelDesign
```

Variables use `{{variable_name}}` interpolation syntax. Import/export libraries as JSON for team sharing.

---

## Schemas & Data Pipelines

All editor windows produce structured JSON conforming to versioned schemas:

| Schema | Source Window | Description |
|--------|-------------|-------------|
| `LevelGraphSchema` | Level Node Graph | Node/edge graph with room types and connections |
| `CutsceneSchema` | Cutscene Timeline | Timeline of dialogue, camera, animation clips |
| `QuestGraphSchema` | Quest Graph | Directed acyclic graph of quest branches |
| `QuestFlowSchema` | Quest Flow Editor | Linear sequence of quest objectives |
| `LootTableSchema` | Loot Balancer | Rarity-tiered drop tables with weights |
| `MetadataTemplateSchema` | Asset Metadata Generator | Custom metadata field definitions |
| `PromptTemplateSchema` | Prompt Library | Prompt entry format for import/export |
| `ConfigProfileSchema` | Config SO | API configuration for serialization |

### Data Pipeline

```
User Input → Editor Window → PromptLibrarySO → AnthropicRuntimeManager
    → Claude API → Structured Response → Schema Validation → JSON Export
        → Runtime Deserialization → Game Objects & Logic
```

---

## Project Structure

```
Assets/
└── AIWorkbench/
    ├── Editor/
    │   ├── Core/             # APIKeyLoader, AnthropicRuntimeManager
    │   ├── Config/           # AnthropicConfigSO, PromptLibrarySO
    │   ├── Windows/          # All EditorWindow classes
    │   ├── Schemas/          # JSON schema definitions
    │   └── Utilities/        # BootstrapWizard, helpers
    ├── Runtime/
    │   └── SchemaRuntime/    # Runtime schema deserialization
    └── Resources/
        ├── DefaultPrompts/   # Built-in prompt library JSON
        └── ConfigProfiles/   # Default configuration profiles
```

---

## Extending the Workbench

### Add a Custom Editor Window

```csharp
[WorkbenchWindow("My Custom Window", "custom-icon")]
public class MyCustomWindow : AIWorkbenchEditorWindow
{
    protected override string GetPromptCategory() => "MyCategory";

    protected override void OnResponseReceived(ClaudeResponse response)
    {
        // Process AI response
        var data = JsonUtility.FromJson<MySchema>(response.Content);
        // Update UI with generated data
    }
}
```

### Add a Custom Schema

```csharp
[System.Serializable]
public class MySchema : IWorkbenchSchema
{
    public string schemaVersion = "1.0";
    public string[] items;

    public bool Validate() => items != null && items.Length > 0;
    public string GetVersion() => schemaVersion;
}
```

Register in `SchemaRegistry` for automatic deserialization support.

---

## Security

- API keys are **never** serialized into assets, scenes, or builds
- Keys are stored in local `.env` files, `EditorPrefs`, or environment variables
- All API communication uses HTTPS
- The workbench is **editor-only** — no runtime API calls ship with your game

---

## Troubleshooting

| Issue | Solution |
|-------|----------|
| "API Key Invalid" | Verify your key starts with `sk-ant-` and has not expired |
| 429 Rate Limit errors | The runtime manager retries automatically with exponential backoff |
| Empty AI responses | Check `MaxTokens` in your config profile — increase if needed |
| Windows not appearing in menu | Reimport the package or run **Assets > Reimport All** |
| Bootstrap Wizard won't launch | Open manually: **Window > AI Workbench > Setup Wizard** |

---

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/my-feature`)
3. Commit your changes (`git commit -am 'Add my feature'`)
4. Push to the branch (`git push origin feature/my-feature`)
5. Open a Pull Request

Please follow the existing code style and include XML documentation for public APIs.

---

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.

---

<p align="center">
  Built with ❤️ by <strong>Randall Price</strong> — powered by <a href="https://www.anthropic.com">Anthropic Claude</a>
</p>
]]>
