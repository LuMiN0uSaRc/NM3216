using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    
    public class TeleType : MonoBehaviour
    {


        //[Range(0, 100)]
        //public int RevealSpeed = 50;

        //private string label01 = "Example <sprite=2> of using <sprite=7> <#ffa000>Graphics Inline</color> <sprite=5> with Text in <font=\"Bangers SDF\" material=\"Bangers SDF - Drop Shadow\">TextMesh<#40a0ff>Pro</color></font><sprite=0> and Unity<sprite=1>";
        //private string label02 = "Example <sprite=2> of using <sprite=7> <#ffa000>Graphics Inline</color> <sprite=5> with Text in <font=\"Bangers SDF\" material=\"Bangers SDF - Drop Shadow\">TextMesh<#40a0ff>Pro</color></font><sprite=0> and Unity<sprite=2>";
        public static TeleType Instance;
        public bool ifFinishRevealing = false;

        private TextMeshProUGUI m_textMeshPro;
        private AudioSource _audioSource;

        void Awake()
        {
            Instance = this;
            // Get Reference to TextMeshPro Component
            m_textMeshPro = GetComponent<TextMeshProUGUI>();
            m_textMeshPro.enableWordWrapping = true;

            _audioSource = gameObject.GetComponent<AudioSource>();
        }


        void Start()
        {
            _audioSource.Play();
            StartCoroutine(RevealText());
        }

        IEnumerator RevealText()
        {
            ifFinishRevealing = false;
            // Force and update of the mesh to get valid information.
            m_textMeshPro.ForceMeshUpdate();

            int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount; // Get # of Visible Character in text object
            int counter = 0;
            int visibleCount = 0;

            while (visibleCount < totalVisibleCharacters)
            {
                visibleCount = counter % (totalVisibleCharacters + 1);

                m_textMeshPro.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

                //// Once the last character has been revealed, wait 1.0 second and start over.
                //if (visibleCount >= totalVisibleCharacters)
                //{
                //    yield return new WaitForSeconds(1.0f);
                //    m_textMeshPro.text = label02;
                //    yield return new WaitForSeconds(1.0f);
                //    m_textMeshPro.text = label01;
                //    yield return new WaitForSeconds(1.0f);
                //}

                counter += 1;
                yield return new WaitForSecondsRealtime(0.02f);
                //yield return null;
            }
            ifFinishRevealing = true;
            _audioSource.Stop();
            //Debug.Log("Done revealing the text.");
        }

        public void RevealAllText()
        {
            StopAllCoroutines();
            _audioSource.Stop();
            ifFinishRevealing = true;
            int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount;
            m_textMeshPro.maxVisibleCharacters = totalVisibleCharacters;
            m_textMeshPro.ForceMeshUpdate();
        }

    }
}