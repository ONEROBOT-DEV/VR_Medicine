using Common.DataManager;
using UnityEngine;

public class LaminarQuestionController : ActionInteractableObject
{
    [SerializeField] private QuestionData[] question;
    [SerializeField] private QuestionPanelController questionPanel;
    private int questionCount;
        
    public void StartQuestion()
    {
        questionPanel.InitializeQuestion(question[questionCount], OnClickTrueAnswer, OnClickFalseAnswer, InvokeEndAction);
    }

    public void ResetQuestionPanel()
    {
        questionCount = 0;
    }

    private void OnClickTrueAnswer()
    {
        InvokeEndAction();
        questionCount++;
    }

    private void OnClickFalseAnswer()
    {
        
        SimulationStaticDataManager.LevelIsPassedWithError(2, "Неправильный ответ на вопрос: " + question[questionCount].Question);
    }
}
