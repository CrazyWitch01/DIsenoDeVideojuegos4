using UnityEngine;

public class BalaPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float Duracion = 1.5f;
    private AudioSource BalaImpactoSFX;
    public AudioClip BalaImpactoSonido;

    void Start()
    {
        GameObject sfxObject = GameObject.Find("BalaImpactoSFX");
        if (sfxObject != null)
        {
            BalaImpactoSFX = sfxObject.GetComponent<AudioSource>();
        }
        Destroy(gameObject, Duracion); 
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemies"))
        {
            if (BalaImpactoSFX != null && BalaImpactoSonido != null)
            {
                BalaImpactoSFX.PlayOneShot(BalaImpactoSonido);
            }
            Destroy(gameObject);
        }
    }
}
