# Face-Recognition
Real-time multi-face detection and biometric recognition desktop system built in C# (.NET) using EmguCV and OpenCV. Features burst-training multi-angle enrollment, dynamic confidence scoring, threshold controls, and privacy-compliant identity management.
# FaceRecPro — Real-Time Face Detection & Recognition Engine

[![Platform](https://img.shields.io/badge/Platform-Windows-blue.svg)](https://www.microsoft.com/windows)
[![Language](https://img.shields.io/badge/Language-C%23%20(.NET%204.0)-purple.svg)](https://docs.microsoft.com/dotnet/csharp/)
[![Framework](https://img.shields.io/badge/Vision-EmguCV%20%2F%20OpenCV-green.svg)](https://www.emgu.com/)
[![License](https://img.shields.io/badge/License-MIT-orange.svg)](LICENSE)

**FaceRecPro** is a high-performance, real-time facial biometric recognition engine built in **C# (.NET)** utilizing **EmguCV** (OpenCV .NET wrapper). Designed for both standalone use and integration into third-party enterprise systems (e.g., attendance systems, door access control, identity verification kiosks).

---

## 🚀 Key Features

- **⚡ Real-Time Multi-Face Detection & Tracking**:
  - Leverages optimized Haar Cascades (`haarcascade_frontalface_default.xml`) for fast face localization across live webcam feeds.
- **🧠 Eigenface Recognition (PCA)**:
  - Uses Principal Component Analysis (PCA) to extract facial biometric features and perform rapid 1:N Euclidean distance matching with zero frame stutter.
- **📸 Multi-Shot / Burst Training Mode**:
  - Captures 5 to 10 photos in rapid succession (~200ms intervals) across varying angles, lighting, and expressions.
  - Substantially increases recognition robustness and prevents false rejections in dynamic environments.
- **🎯 Real-Time Confidence Scoring & Sensitivity Slider**:
  - Computes and displays dynamic match confidence percentages directly on green bounding boxes (e.g., `Akash (94%)`) or `Unknown` (red box).
  - Built-in live Sensitivity Slider: Switch between **Strict** (high security), **Balanced**, or **Loose** matching modes on the fly.
- **🛡️ Privacy-First "List of Recognition" Dashboard**:
  - **Single Registration Principle**: Multi-burst shots are intelligently grouped so each enrolled person appears **strictly once** (e.g., `8 shots (Burst)`).
  - **Zero Image Exposure**: Raw face photographs remain completely hidden from the table view to respect biometric privacy standards.
  - **Full Profile Management**: Rename an enrolled person across all their burst samples in one click, or delete a profile with automatic model and file re-indexing.
  - Real-time search filter and aggregate enrollment statistics.

---

## 🛠️ Architecture & Core Components

| File | Purpose |
| :--- | :--- |
| **`MainForm.cs`** | Core engine: webcam streaming, frame grabber, detection, recognition loop, burst capture, and profile operations. |
| **`EigenObjectRecognizer.cs`** | Mathematical Eigenface (PCA) projection engine and distance calculation. |
| **`RecognitionListForm.cs`** | Management dashboard for enrolled persons (aggregation, update, delete, search). |
| **`Program.cs`** | Application bootstrap and exception handling. |

---

## 💡 Real-World Applications

This engine can be deployed or embedded into:
1. **Automated Attendance Systems**: Contactless check-in for corporate offices, colleges, and construction sites.
2. **Access Control & Turnstiles**: Integration with electronic gates and door relays.
3. **Smart Reception / Kiosks**: Personalized greetings and identity verification for enrolled visitors.
4. **Security & Watchlist Monitoring**: Live detection and unknown individual alerts.

---

## 🔌 Integration into Other Projects

The recognition core and data models (`RegisteredPerson`, `UpdatePersonName`, `DeletePerson`, `EigenObjectRecognizer`) are cleanly decoupled. You can easily reference `MultiFaceRec.exe` or port `MainForm.cs` methods into:
- WPF or ASP.NET backends
- WinForms enterprise ERP / HR management suites
- Windows services connected to IP surveillance cameras

---

## 📦 Getting Started

### Prerequisites
- Windows 7 / 8 / 10 / 11
- .NET Framework 4.0 or higher
- Webcam / USB camera

### Building the Project
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/FaceRecPro.git
   cd FaceRecPro
