using Newtonsoft.Json;
using UnityEngine;

public class CharacterReal
{
	public CharacterData data;

    [JsonIgnore]
	public Sprite GetSprite => Resources.Load<Sprite>(string.Format(Defines.FormatIconPath, data.Icon));
    [JsonIgnore]
    public string GetName => DataTableManager.GetStringTable().Get(data.Name);

	public int ID { get; set; }
	public int level;


	public CharacterReal()
	{
		ID = Animator.StringToHash(System.DateTime.Now.Ticks.ToString());
	}

}