using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


//User defined dictionaries;

//EPoolType Enum => Pooled item Gameobject
[Serializable]
public class PoolTableDictionnary : SerializableDictionary<EPoolType, GameObject> { }

//Audioclips
[Serializable]
public class StringAudioClipDictionary : SerializableDictionary<string, AudioClip> { }


//Examples of how to use;
[Serializable]
public class StringStringDictionary : SerializableDictionary<string, string> { }

[Serializable]
public class ColorArrayStorage : SerializableDictionary.Storage<Color[]> { }

[Serializable]
public class StringColorArrayDictionary : SerializableDictionary<string, Color[], ColorArrayStorage> { }
