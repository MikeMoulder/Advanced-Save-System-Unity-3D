using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;

public class SaveManager : MonoBehaviour
{
	protected const string hash = "%$&O)%$&%&$%&$%^*66";

	public static string Encrypt(string input)
	{
		byte[] data = UTF8Encoding.UTF8.GetBytes(input);
		using(MD5CryptoServiceProvider md5=new MD5CryptoServiceProvider())
		{
			byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
			using(TripleDESCryptoServiceProvider trip=new TripleDESCryptoServiceProvider() { Key=key,Mode=CipherMode.ECB,Padding=PaddingMode.PKCS7})
			{
				ICryptoTransform tr = trip.CreateEncryptor();
				byte[] result = tr.TransformFinalBlock(data, 0, data.Length);
				return Convert.ToBase64String(result, 0, result.Length);
			}
		}
	}
	public static string Decrypt(string input)
	{
		byte[] data = Convert.FromBase64String(input);
		using(MD5CryptoServiceProvider md5=new MD5CryptoServiceProvider())
		{
			byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
			using(TripleDESCryptoServiceProvider trip=new TripleDESCryptoServiceProvider() { Key=key,Mode=CipherMode.ECB,Padding=PaddingMode.PKCS7})
			{
				ICryptoTransform tr = trip.CreateDecryptor();
				byte[] result = tr.TransformFinalBlock(data, 0, data.Length);
				return UTF8Encoding.UTF8.GetString(result);
			}
		}
	}

	public static void SaveString(string SaveName, string value)
	{
		PlayerPrefs.SetString(Encrypt(SaveName), Encrypt(value));
	}
	public static void SaveInt(string SaveName, int value)
	{
		PlayerPrefs.SetString(Encrypt(SaveName), Encrypt(value.ToString()));
	}

	public static void SaveFloat(string SaveName, float value)
	{
		PlayerPrefs.SetString(Encrypt(SaveName), Encrypt(value.ToString()));
	}

	public static int LoadInt(string SaveName)
	{
		int value;
		string _encryption = Encrypt(SaveName);
		string _final;
		if (PlayerPrefs.HasKey(_encryption))
		{
			_final = PlayerPrefs.GetString(_encryption);
		}else
		{
			SaveFloat(SaveName, 0);
			_final = PlayerPrefs.GetString(_encryption);
		}
		string _finalDecryption = Decrypt(_final);
		value = int.Parse(_finalDecryption);
		return value;
	}
	public static float LoadFloat(string SaveName)
	{
		float value;
		string _encryption = Encrypt(SaveName);
		string _final;
		if (PlayerPrefs.HasKey(_encryption))
		{
			_final = PlayerPrefs.GetString(_encryption);
		}else
		{
			SaveFloat(SaveName, 0.0f);
			_final = PlayerPrefs.GetString(_encryption);
		}
		string _finalDecryption = Decrypt(_final);
		value = float.Parse(_finalDecryption);
		return value;
	}

	public static string LoadString(string SaveName)
	{
		string value;
		string _encryption = Encrypt(SaveName);
		string _final;
		if (PlayerPrefs.HasKey(_encryption))
		{
			_final = PlayerPrefs.GetString(_encryption);
		}else
		{
			SaveString(SaveName, " ");
			_final = PlayerPrefs.GetString(_encryption);
		}
		string _finalDecryption = Decrypt(_final);
		value = _finalDecryption;
		return value;
	}

	public static void SaveVector3(string SaveName,Vector3 value)
	{
		//SaveX
		PlayerPrefs.SetString(Encrypt(SaveName + "X"), Encrypt(value.x.ToString()));

		//SaveY
		PlayerPrefs.SetString(Encrypt(SaveName + "Y"), Encrypt(value.y.ToString()));

		//SaveZ
		PlayerPrefs.SetString(Encrypt(SaveName + "Z"), Encrypt(value.z.ToString()));
	}

	public static Vector3 LoadVector3(string SaveName)
	{
		float Xvalue;
		string _Xencryption = Encrypt(SaveName + "X");
		string _Xfinal;
		if (PlayerPrefs.HasKey(_Xencryption))
		{
			_Xfinal = PlayerPrefs.GetString(_Xencryption);
		}else
		{
			SaveVector3(SaveName, Vector3.zero);
			_Xfinal = PlayerPrefs.GetString(_Xencryption);
		}
		string _XfinalDecryption = Decrypt(_Xfinal);
		Xvalue = float.Parse(_XfinalDecryption);

		float Yvalue;
		string _Yencryption = Encrypt(SaveName + "Y");
		string _Yfinal = PlayerPrefs.GetString(_Yencryption);
		string _YfinalDecryption = Decrypt(_Yfinal);
		Yvalue = float.Parse(_YfinalDecryption);

		float Zvalue;
		string _Zencryption = Encrypt(SaveName + "Z");
		string _Zfinal = PlayerPrefs.GetString(_Zencryption);
		string _ZfinalDecryption = Decrypt(_Zfinal);
		Zvalue = float.Parse(_ZfinalDecryption);

		Vector3 value = new Vector3(Xvalue, Yvalue, Zvalue);
		return value;
	}


	//Save Vector 2
	public static void SaveVector2(string SaveName, Vector2 value)
	{
		//SaveX
		PlayerPrefs.SetString(Encrypt(SaveName + "X"), Encrypt(value.x.ToString()));

		//SaveY
		PlayerPrefs.SetString(Encrypt(SaveName + "Y"), Encrypt(value.y.ToString()));
	}

	public static Vector2 LoadVector2(string SaveName)
	{
		float Xvalue;
		string _Xencryption = Encrypt(SaveName + "X");
		string _Xfinal;
		if (PlayerPrefs.HasKey(_Xencryption))
		{
			_Xfinal = PlayerPrefs.GetString(_Xencryption);
		}else
		{
			SaveVector2(SaveName, Vector2.zero);
			_Xfinal = PlayerPrefs.GetString(_Xencryption);
		}
		string _XfinalDecryption = Decrypt(_Xfinal);
		Xvalue = float.Parse(_XfinalDecryption);

		float Yvalue;
		string _Yencryption = Encrypt(SaveName + "Y");
		string _Yfinal = PlayerPrefs.GetString(_Yencryption);
		string _YfinalDecryption = Decrypt(_Yfinal);
		Yvalue = float.Parse(_YfinalDecryption);

		Vector2 value = new Vector3(Xvalue, Yvalue);
		return value;
	}

	public static void DeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}
	public static void DeleteKey(string saveName)
	{
		string _key = Encrypt(saveName);
		PlayerPrefs.DeleteKey(_key);
	}
}
