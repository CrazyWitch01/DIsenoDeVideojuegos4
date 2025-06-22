using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraEffects : MonoBehaviour
{
    public Volume globalVolume;

    [Header("Efectos de Daño")]
    public float damageBloomIntensity = 5f; // Bloom
    public float damageVignetteIntensity = 0.6f; // Viñeta
    public Color damageVignetteColor = new Color(0.5f, 0f, 0f, 1f); // Color de la viñeta (rojo)

    public float damageSaturation = 50f;

    public float effectDuration = 0.5f; // Duración del efecto

    private Bloom bloom;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;

    private float originalBloomIntensity;
    private float originalVignetteIntensity;
    private Color originalVignetteColor;
    private float originalSaturation;

    private bool isEffectActive = false;
    private float effectTimer = 0f;

    void Start()
    {
        if (globalVolume.profile.TryGet(out bloom))
        {
            originalBloomIntensity = bloom.intensity.value;
        }
        if (globalVolume.profile.TryGet(out vignette))
        {
            originalVignetteIntensity = vignette.intensity.value;
            originalVignetteColor = vignette.color.value;
        }
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            originalSaturation = colorAdjustments.saturation.value;
        }
    }

    void Update()
    {
        if (isEffectActive)
        {
            effectTimer += Time.deltaTime;
            if (effectTimer >= effectDuration)
            {
                ResetPostProcessing();
                isEffectActive = false;
            }
            else
            {
                float t = effectTimer / effectDuration;
                if (bloom != null)
                {
                    bloom.intensity.value = Mathf.Lerp(damageBloomIntensity, originalBloomIntensity, t);
                }
                if (vignette != null)
                {
                    vignette.intensity.value = Mathf.Lerp(damageVignetteIntensity, originalVignetteIntensity, t);
                    vignette.color.value = Color.Lerp(damageVignetteColor, originalVignetteColor, t);
                }
                if (colorAdjustments != null)
                {
                    colorAdjustments.saturation.value = Mathf.Lerp(damageSaturation, originalSaturation, t);
                }
            }
        }
    }

    // Llamar a esta función cuando el jugador reciba daño
    public void ActivarEfectoSangre()
    {
        if (globalVolume == null) return;

        if (bloom != null)
        {
            bloom.intensity.value = damageBloomIntensity;
            bloom.active = true; 
        }
        if (vignette != null)
        {
            vignette.intensity.value = damageVignetteIntensity;
            vignette.color.value = damageVignetteColor;
            vignette.active = true; 
        }
        if (colorAdjustments != null)
        {
            colorAdjustments.saturation.value = damageSaturation;
            colorAdjustments.active = true; 
        }

        isEffectActive = true;
        effectTimer = 0f;
    }

    // Resetea el post-procesado
    private void ResetPostProcessing()
    {
        if (bloom != null)
        {
            bloom.intensity.value = originalBloomIntensity;
        }
        if (vignette != null)
        {
            vignette.intensity.value = originalVignetteIntensity;
            vignette.color.value = originalVignetteColor;
        }
        if (colorAdjustments != null)
        {
            colorAdjustments.saturation.value = originalSaturation;
        }
    }
}