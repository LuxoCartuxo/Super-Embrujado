using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEvaluation : MonoBehaviour
{
    #region REFERENCES
    [Header("DIÁLOGOS DE FINALIZACIÓN")]
    public ConversationScriptable victoryConversation;
    public ConversationScriptable defeatConversation;
    public CheckoutRegister register;

    [Header("CONFIGURACIÓN DE ESCENA FINAL")]
    [Tooltip("Escribe el nombre exacto de la escena de tu Menú Principal")]
    public string MainMenuSceneName = "MainMenuScene";
    #endregion

    public void TriggerEvaluationWithDelay(float delayInSeconds)
    {
        StartCoroutine(EvaluateRoutine(delayInSeconds));
    }

    private IEnumerator EvaluateRoutine(float delay)
    {
        print($"[JEFE] Aguardando {delay} segundos para evaluar la compra...");
        yield return new WaitForSeconds(delay);

        EvaluatePurchase();
    }

    private void EvaluatePurchase()
    {
        if (BossManager.Instance == null || ShoppingCart.Instance == null) return;

        List<ItemScriptable> requested = BossManager.Instance.requestedList;
        List<ItemScriptable> bought = ShoppingCart.Instance.itemsInCart;

        bool isCorrect = CheckIfListsMatch(requested, bought);

        if (isCorrect)
        {
            print("[EVALUACIÓN] ¡VICTORIA! La compra fue perfecta.");
            if (victoryConversation != null && DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartConversation(victoryConversation);
                StartCoroutine(WaitAndReturnToMainMenu());
            }
        }
        else
        {
            print("[EVALUACIÓN] DERROTA. La compra no coincide con la lista del Jefe.");
            if (defeatConversation != null && DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartConversation(defeatConversation);
                StartCoroutine(WaitAndReturnToMainMenu());
            }
        }
    }

    private IEnumerator WaitAndReturnToMainMenu()
    {
        yield return null;

        while (DialogueManager.Instance != null && DialogueManager.Instance.IsTalking())
        {
            yield return null;
        }

        print("[SISTEMA] Diálogo finalizado. Regresando al Menú Principal...");

        yield return new WaitForSeconds(1.0f);

        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(MainMenuSceneName);
    }

    private bool CheckIfListsMatch(List<ItemScriptable> requested, List<ItemScriptable> bought)
    {
        if (requested.Count != bought.Count) return false;

        List<ItemScriptable> tempBought = new List<ItemScriptable>(bought);

        for (int i = 0; i < requested.Count; i++)
        {
            if (tempBought.Contains(requested[i]))
            {
                tempBought.Remove(requested[i]);
            }
            else
            {
                return false;
            }
        }

        return tempBought.Count == 0;
    }
}