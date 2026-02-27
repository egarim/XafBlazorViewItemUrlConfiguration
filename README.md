# XAF Copilot Standalone - Three Ways to Integrate AI Chat

This project demonstrates **three different approaches** to integrate GitHub Copilot SDK AI chat into a DevExpress XAF Blazor application.

## 🎯 Three Approaches Demonstrated

### 1️⃣ **XAF ViewItem** (Traditional XAF Integration)
- **What:** AI chat embedded as an XAF ViewItem
- **Access:** Via XAF navigation after login
- **Security:** Full XAF security integration
- **Use case:** Internal business app with full XAF features
- **URL:** `https://yourapp.com/` → Login → Navigate to "AI Assistant"

### 2️⃣ **Standalone Blazor Page** (Direct URL Access)
- **What:** `/chat` page accepting URL parameters
- **Access:** Direct URL with optional pre-populated messages
- **Security:** Optional (can be public or token-protected)
- **Use case:** Shareable chat links, embeddable widget, simple AI interface
- **URL Examples:**
  - `https://yourapp.com/chat`
  - `https://yourapp.com/chat?message=Hello`
  - `https://yourapp.com/chat?message=Show order 123&context=order123`

### 3️⃣ **REST API Endpoint** (Programmatic Access)
- **What:** HTTP POST endpoint for AI chat
- **Access:** REST API call from any client
- **Security:** Token-based auth (configurable)
- **Use case:** Mobile apps, external systems, automation scripts
- **Endpoint:** `POST /api/chat`

---

## 🚀 Quick Start

### Prerequisites
- .NET 9.0 SDK
- GitHub Copilot subscription (for API access)
- DevExpress license (XAF + Blazor components)

### 1. Clone & Restore
```bash
git clone https://github.com/egarim/XafCopilotStandalone.git
cd XafCopilotStandalone
dotnet restore
```

### 2. Configure GitHub Copilot
Edit `appsettings.json` in both Blazor.Server and WebApi projects:

```json
{
  "GitHubCopilot": {
    "ApiKey": "your-github-copilot-api-key",
    "Model": "gpt-4o"
  }
}
```

### 3. Run
```bash
# Blazor Server (includes XAF + standalone page)
cd XafCopilotStandalone.Blazor.Server
dotnet run

# Web API (REST endpoint)
cd XafCopilotStandalone.WebApi
dotnet run
```

---

## 📖 Usage Guide

### Approach 1: XAF ViewItem

**1. Login to XAF application**
```
https://localhost:5001
Username: Admin
Password: (empty)
```

**2. Navigate to AI Assistant**
- Main Menu → "Copilot Chat" (or wherever you've placed the ViewItem)

**3. Ask questions**
```
"List all orders"
"Create a new employee named John Doe"
"Show me today's sales"
```

**Features:**
- ✅ Full XAF security (user permissions)
- ✅ Access to XAF Object Space (can query/create business objects)
- ✅ Integrated into XAF UI
- ✅ Conversation history persisted per user

---

### Approach 2: Standalone Page

**Direct Access (no login required):**
```
https://localhost:5001/chat
```

**With pre-populated message:**
```
https://localhost:5001/chat?message=Hello, who are you?
```

**With context data:**
```
https://localhost:5001/chat?message=Show me this order&context=order123
```

**URL Parameters:**
| Parameter | Type | Description | Example |
|---|---|---|---|
| `message` | string | Pre-populate initial message | `?message=Hello` |
| `context` | string | Pass context data | `?context=userId:123` |

**Features:**
- ✅ No login required (configurable)
- ✅ Shareable links
- ✅ URL parameters for automation
- ✅ Clean, standalone UI
- ✅ Can be embedded in iframe

**Example use cases:**
- Share a chat link: "Click here to ask AI about your order"
- Bookmarklet: `javascript:window.open('/chat?message='+encodeURIComponent(window.getSelection()))`
- QR code on receipts → `/chat?message=Track order&context=order123`

---

### Approach 3: REST API

**Endpoint:**
```
POST https://localhost:5002/api/chat
```

**Request Body:**
```json
{
  "message": "List all orders",
  "context": {
    "userId": "123",
    "orderId": "456"
  },
  "history": [
    {
      "role": "user",
      "content": "Previous question"
    },
    {
      "role": "assistant",
      "content": "Previous answer"
    }
  ]
}
```

**Response:**
```json
{
  "message": "Here are all the orders:\n\n1. Order #123...",
  "timestamp": "2026-02-27T10:00:00Z",
  "model": "gpt-4o",
  "tokensUsed": 245
}
```

**cURL Example:**
```bash
curl -X POST https://localhost:5002/api/chat \
  -H "Content-Type: application/json" \
  -d '{
    "message": "List all orders",
    "context": {
      "userId": "123"
    }
  }'
```

**C# Client Example:**
```csharp
using var client = new HttpClient();
var request = new ChatRequest
{
    Message = "List all orders",
    Context = new Dictionary<string, string>
    {
        ["userId"] = "123"
    }
};

var response = await client.PostAsJsonAsync(
    "https://localhost:5002/api/chat",
    request
);

var result = await response.Content.ReadFromJsonAsync<ChatResponse>();
Console.WriteLine(result.Message);
```

**Features:**
- ✅ Language-agnostic (REST)
- ✅ Stateless (pass conversation history if needed)
- ✅ JSON request/response
- ✅ Token tracking
- ✅ Error handling
- ✅ Health check endpoint

**Example use cases:**
- Mobile app integration
- Automation scripts
- External system integration
- Chatbot platforms (Telegram, Discord, Slack)

---

## 🏗️ Project Structure

```
XafCopilotStandalone/
├── XafCopilotStandalone.Blazor.Server/     # Blazor Server App
│   ├── Editors/
│   │   └── CopilotChatViewItem/            # XAF ViewItem (Approach 1)
│   │       ├── CopilotChat.razor           # Shared chat component
│   │       └── CopilotChatViewItemBlazor.cs
│   └── Pages/
│       └── Chat.razor                       # Standalone page (Approach 2)
│
├── XafCopilotStandalone.WebApi/            # Web API Project
│   └── Controllers/
│       └── ChatController.cs                # REST API (Approach 3)
│
├── XafCopilotStandalone.Module/            # Shared Module
│   └── Services/
│       ├── CopilotChatService.cs           # GitHub Copilot SDK integration
│       ├── CopilotChatClient.cs            # IChatClient implementation
│       └── CopilotChatDefaults.cs          # Prompts & config
│
└── XafCopilotStandalone.Module.Blazor/     # Blazor-specific module
```

---

## 🔧 Configuration

### GitHub Copilot API Key

**Option 1: appsettings.json**
```json
{
  "GitHubCopilot": {
    "ApiKey": "your-api-key",
    "Model": "gpt-4o",
    "Temperature": 0.7
  }
}
```

**Option 2: Environment Variable**
```bash
export GITHUB_COPILOT_API_KEY=your-api-key
```

**Option 3: User Secrets** (recommended for dev)
```bash
dotnet user-secrets set "GitHubCopilot:ApiKey" "your-api-key"
```

### Security Configuration

**Standalone Page Access Control:**

Edit `Chat.razor` to add authentication:
```csharp
@attribute [Authorize] // Require login
```

**API Endpoint Authentication:**

Edit `ChatController.cs`:
```csharp
[Authorize] // Require token
[ApiController]
public class ChatController : ControllerBase
{
    // ...
}
```

---

## 📊 Comparison Table

| Feature | XAF ViewItem | Standalone Page | REST API |
|---|---|---|---|
| **Login Required** | ✅ Yes | ⚙️ Optional | ⚙️ Optional |
| **XAF Integration** | ✅ Full | ❌ No | ❌ No |
| **URL Parameters** | ❌ No | ✅ Yes | N/A |
| **Embeddable** | ❌ No | ✅ Yes | N/A |
| **Programmatic Access** | ❌ No | ❌ No | ✅ Yes |
| **Conversation Persistence** | ✅ Per user | ❌ Session only | ❌ Stateless |
| **Mobile Friendly** | ⚙️ Depends | ✅ Yes | ✅ Yes |
| **External System Integration** | ❌ No | ⚙️ Limited | ✅ Yes |

---

## 🎨 Customization

### Change AI Model
```csharp
// In CopilotChatService.cs
builder.UseChatClient(client => client
    .UseGitHubCopilot(apiKey, modelId: "gpt-4o") // Change model here
);
```

### Custom System Prompt
```csharp
// In CopilotChatDefaults.cs
public static string SystemPrompt =>
    "You are a helpful AI assistant for our e-commerce platform. " +
    "You have access to orders, products, and customer data.";
```

### Add Custom Prompt Suggestions
```csharp
// In CopilotChatDefaults.cs
public static List<PromptSuggestion> PromptSuggestions => new()
{
    new("Orders", "Show today's orders", "List all orders placed today"),
    new("Sales", "Sales report", "Generate a sales report for this month"),
    new("Customers", "Top customers", "Who are our top 10 customers?")
};
```

---

## 🧪 Testing

### Test Standalone Page
```bash
# Open browser
https://localhost:5001/chat?message=Hello

# Or use curl
curl "https://localhost:5001/chat?message=Hello"
```

### Test REST API
```bash
curl -X POST https://localhost:5002/api/chat \
  -H "Content-Type: application/json" \
  -d '{"message": "Hello, who are you?"}'
```

### Health Check
```bash
curl https://localhost:5002/api/chat/health
```

---

## 📚 Articles

This project is based on the implementation described in:
- [The Day I Integrated GitHub Copilot SDK Inside My XAF App — Part 1](https://www.jocheojeda.com/2026/02/16/the-day-i-integrated-github-copilot-sdk-inside-my-xaf-app-part-1/)
- [The Day I Integrated GitHub Copilot SDK Inside My XAF App — Part 2](https://www.jocheojeda.com/2026/02/16/the-day-i-integrated-github-copilot-sdk-inside-my-xaf-app-part-2/)

---

## 🤝 Contributing

Contributions welcome! Open an issue or PR.

---

## 📄 License

MIT License - see LICENSE.txt

---

## 🔗 Links

- [DevExpress XAF](https://www.devexpress.com/products/net/application_framework/)
- [GitHub Copilot SDK](https://github.com/features/copilot)
- [Microsoft.Extensions.AI](https://github.com/dotnet/extensions)

---

**Made with ❤️ by [Joche Ojeda](https://www.jocheojeda.com)**
