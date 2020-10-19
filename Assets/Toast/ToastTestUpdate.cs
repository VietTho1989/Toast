using UnityEngine;
using System.Text;
using System.Collections;

public class ToastTestUpdate : MonoBehaviour
{

    private Coroutine checkToast;

    public IEnumerator TaskCheckToast()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            int random = Random.Range(2, 200);
            StringBuilder builder = new StringBuilder();
            {
                for (int i = 0; i < random; i++)
                {
                    builder.Append("" + i);
                }
            }
            Toast.showMessage(builder.ToString());
        }
    }

    void OnEnable()
    {
        StartCoroutine("TaskCheckToast");
    }

    void OnDisable()
    {

    }

    void OnDestroy()
    {

    }

}