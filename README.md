# MessagePackEditor

![summary-image](https://enrike3.blob.core.windows.net/images/Union2.PNG)

## is 何

MessagePack for C#向けの型をUnityのアセットとして保存、編集するためのエディタ拡張です。

## 使い方

### 前提

このライブラリはMessagePack for C#に依存しています。
MessagePack-CSharpが導入されていない環境では使用できません。

### 使ってみる

保存したり編集したいMessagePackの型が`Hoge`だったとします。

```csharp
using MessagePack;

[MessagePackObject]
public class Hoge
{
    [Key(0)]
    public int HogeId { get; set; }
}
```

`MessagePackScriptableObject<Hoge>`を継承した型を作成します。

```csharp
using UnityEngine;
using MessagePackEditor;

[CreateAssetMenu(menuName ="HogeObject")]
public class HogeObject : MessagePackScriptableObject<Hoge>
{
}
```

`MessagePackScriptableObject<Hoge>`を継承したクラスはScriptableObjectになるので、
CreateAssetMenuなんかをつけてあげるとアセットの生成が楽になります。

あとは生成したアセットをインスペクタでいじってどうぞ
