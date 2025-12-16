using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace StealthGame
{
    public class MyMainMenu : MonoBehaviour
    {
        private UIDocument m_UIDocument;

        private Button m_StartButton;
        private Button m_ExitButton;
        private VisualElement background;
        private VisualElement selectionScreen;
        private Button johnButton;
        private Button ivanButton;
    
        private void Awake()
        {
            m_UIDocument = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            m_StartButton = m_UIDocument.rootVisualElement.Q<Button>("StartButton");
            m_ExitButton = m_UIDocument.rootVisualElement.Q<Button>("ExitButton");
            background = m_UIDocument.rootVisualElement.Q<VisualElement>("Background");
            selectionScreen = m_UIDocument.rootVisualElement.Q<VisualElement>("SelectionScreen");
            ivanButton = m_UIDocument.rootVisualElement.Q<Button>("IvanButton");
            johnButton = m_UIDocument.rootVisualElement.Q<Button>("JohnButton");

            m_StartButton.clicked += () =>
            {
                background.style.display = DisplayStyle.None;
                selectionScreen.style.display = DisplayStyle.Flex;

                johnButton.clicked += () =>
                {
                    GameSettings.character = 1; // Jon Lemon
                    LoadScene(1);
                };

                ivanButton.clicked += () =>
                {
                    GameSettings.character = 2; // Ivan
                    LoadScene(1);
                };
            };

            m_ExitButton.clicked += () =>
            {
                Application.Quit();
            };
        }

        private void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}