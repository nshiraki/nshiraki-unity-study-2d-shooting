using UnityEngine;
// 使用方法
// 
// Sound sound;
// sound = GameObject.Find("Sound").GetComponent<Sound>();
// sound.Play(1);
public class Sound : MonoBehaviour
{
	AudioSource audioSource;

	public AudioClip[] clips;

	// Start is called before the first frame update
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void Play(int num)
	{
		AudioClip clip = clips[num];

		if (clip != null)
		{
			audioSource.PlayOneShot(clip);
		}
	}
}
