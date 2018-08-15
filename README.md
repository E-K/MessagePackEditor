# MessagePackEditor

## 使い方

### 前提

このライブラリはMessagePack-CSharpに依存しています。
MessagePack-CSharpが導入されていない以下のような`H

### Getting Started

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
