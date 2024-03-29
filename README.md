# Unity_Texture2DArrayGenerator

Texture2DArrayGenerator provides function or editor to make a ``Texture2DArray``.

## Importing

You can use Package Manager or import it directly.

```
https://github.com/XJINE/Unity_Texture2DArrayGenerator.git?path=Assets/Packages/Texture2DArrayGenerator
```

### Dependencies

This project use following resources.

- https://github.com/XJINE/Unity_AssetCreationHelper

## How to Use

### Use in Runtime

``Texture2DArrayGenerator`` is quite simple. Pass some textures to ``Generate`` function.
And then, it returns ``Texture2DArray`` instance.

```csharp
public static Texture2DArray Texture2DArrayGenerator.Generate(IList<Texture2D> textures)
```

### Use as Asset

<img src="https://github.com/XJINE/Unity_Texture2DArrayGenerator/blob/main/Screenshot.png" width="100%" height="auto" />

If you need ``Texture2DArray`` as pre-compiled asset. It can be made from CustomWindow.

Pass the textures to CustomWindow and press the Generate button.
Then, Texture2D asset will be generated in current ``Assets`` dir.

If you succeed in generate, a popup will be shown in CustomWindow.
If you failed to generate, some error message will be shown in Console.
