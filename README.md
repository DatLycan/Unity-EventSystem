
<h1 align="left">Unity C# Event System</h1>

<div align="left">

[![Status](https://img.shields.io/badge/status-active-success.svg)]()
[![GitHub Issues](https://img.shields.io/github/issues/datlycan/Unity-EventSystem.svg)](https://github.com/DatLycan/Unity-EventSystem/issues)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](/LICENSE)

</div>

---

<p align="left"> Custom Event System for Unity that allows communication between different scripts without direct references.
    <br> 
</p>

## 📝 Table of Contents

- [Getting Started](#getting_started)
- [Usage](#usage)
- [Acknowledgments](#acknowledgement)

## 🏁 Getting Started <a name = "getting_started"></a>

### Installing

1. Install the [Git Dependency Resolver](https://github.com/mob-sakai/GitDependencyResolverForUnity).
2. Import it in Unity with the Unity Package Manager using this URL:<br>
   ``https://github.com/DatLycan/Unity-EventSystem.git``

## 🎈 Usage <a name="usage"></a>


   ```C#
    public struct MyEvent : IEvent {
        public bool ShouldLog() => true;
    }
   ```
   ```C#
    public class MyClassA : MonoBehaviour {
        private void Start() {
            EventBus<MyEvent>.Raise(new MyEvent());
        }
    }
   ```
   ```C#
    public class MyClassB : MonoBehaviour {
        private EventBinding<MyEvent> testEventBinding;
    
        private void OnEnable() {
            testEventBinding = new EventBinding<MyEvent>(OnTestEvent);
            EventBus<MyEvent>.Register(testEventBinding);
        }
    
        private void OnDisable() {
            EventBus<MyEvent>.Unregister(testEventBinding);
        }
    
        private void OnTestEvent() {
            Debug.Log("Event received!");
        }
    }
   ```
---



## 🎉 Acknowledgements <a name = "acknowledgement"></a>

- *Snippets taken from [adammyhre's Unity-Event-Bus project](https://github.com/adammyhre/Unity-Event-Bus).*

