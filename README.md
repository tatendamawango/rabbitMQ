# 🧪 Distributed Container Simulation using RabbitMQ

This project simulates a pressure controlled container system using distributed C# clients and a message based RPC server built on RabbitMQ. The system includes real-time interaction between input/output clients and a central server via RPC messaging. Clients either increase or decrease the mass of a container based on internal pressure logic, with automatic resets upon implosion or explosion thresholds.

---

## 🧩 Architecture Overview

```
+-------------------+            +-------------------+            +-------------------+
|   Input Client    |  <=====>   |    RabbitMQ Bus   |  <=====>   |   Output Client   |
+-------------------+            +-------------------+            +-------------------+
                                        ||
                                  +----------------+
                                  |     Server     |
                                  | ContainerLogic |
                                  +----------------+
```

---

## 🔧 Features

- 🧪 Thread-safe simulation of container gas dynamics
- 🌡 Real-time updates of mass, pressure, and temperature
- 🧠 RPC communication over RabbitMQ
- 🟢 Input client adds gas when pressure is low
- 🔴 Output client removes gas when pressure is high
- 💥 Automatic simulation reset upon explosion (>100,000 Pa) or implosion (<85,000 Pa)
- 📊 Logs with NLog
- 🛠 Management via RabbitMQ web console

---

## ⚙️ How It Works

- The **server** (`ContainerService`) listens for incoming RPC messages via RabbitMQ and manages the simulation.
- **Clients** (input/output) connect to the server, determine their role using `ActiveClient()`, and act accordingly:
  - If role is `1`, input client adds random mass.
  - If role is `2`, output client subtracts random mass.
- The server uses a background thread to update:
  - Container pressure = `(mass * temperature * gasConstant) / volume`
  - Simulation state every 2 seconds.

---

## 🗂️ File Structure

```
/Clients/
├── Client.cs                # Input or output client
├── ContainerClient.cs       # RPC wrapper (calls the server)

/Servers/
├── Server.cs                # Entry point for the RPC server
├── ContainerService.cs      # RPC endpoint handler
├── ContainerLogic.cs        # Simulation logic

/Services/
├── RPCMessage.cs            # Request/response format
├── IContainerService.cs     # Shared interface and data contracts
```

---

## 📈 Console Sample

```
12:34:01|INFO| Input client actives
12:34:03|INFO| Mass addition: 4.85
12:34:03|INFO| Pressure was 89012.41
...
12:34:20|INFO| Container Exploded. Simulation is Reset
```

---

## 🚀 Running the Project

### 1️⃣ Start RabbitMQ
Make sure RabbitMQ is installed and running.  
Management console: [http://localhost:15672](http://localhost:15672)  
Login: `guest` / `guest`  
➡️ Docs: https://www.rabbitmq.com/management.html

### 2️⃣ Start the Server
```bash
dotnet run --project Server.cs
```

### 3️⃣ Start Input or Output Clients
You can run multiple clients concurrently.
```bash
dotnet run --project Client.cs
```

---

## 🔗 Technologies Used

- C#
- RabbitMQ (with direct exchanges)
- Newtonsoft.Json
- NLog (console logging)
- Threads and synchronization
