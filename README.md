# FrameX360

Ferramenta Windows para aplicar patches em ficheiros **XEX** (Xbox 360). Permite descriptografar e descomprimir XEX (RGH/JTAG), aplicar patches de FPS e compatibilidade a partir da pasta local ou do repositório [xenia-canary/game-patches](https://github.com/xenia-canary/game-patches).

**FrameX360** — by [@honest](https://github.com/honest)

---

## Funcionalidades

- **Descriptografar / descomprimir XEX** — suporte a ficheiros RGH/JTAG (requer `xextool.exe`)
- **Patches de FPS e compatibilidade** — aplicação de patches no formato TOML (Xenia) ou built-in
- **Patches locais** — leitura da pasta `Patches` (ficheiros `.patch.toml`)
- **Busca no GitHub** — obtenção e pesquisa de patches no repositório [xenia-canary/game-patches](https://github.com/xenia-canary/game-patches) por nome de jogo
- **Detecção por Title ID** — reconhecimento automático do jogo a partir do ficheiro XEX
- **Backup automático** — criação de `.bak` antes de aplicar patches
- **Interface bilingue** — português e inglês; idioma guardado entre sessões
- **Instalador** — setup opcional para instalação em `C:\FrameX360`

---

## Requisitos

- **Windows**
- [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)
- [.NET SDK](https://dotnet.microsoft.com/download) (apenas para compilar a partir do código fonte)

Para **decrypt/descompressão** de XEX é necessário colocar `xextool.exe` na mesma pasta do executável.

---

## Instalação

1. Descarregue a [última release](https://github.com/honest/FrameX360/releases) ou clone o repositório.
2. Extraia e execute `FrameX360.exe`.
3. (Opcional) Coloque `xextool.exe` na pasta do executável para poder descriptografar/descomprimir XEX.

---

## Compilar a partir do código fonte

```bash
git clone https://github.com/honest/FrameX360.git
cd FrameX360
dotnet build XToolRGH.csproj -c Release
```

O executável é gerado em `bin\Release\net48\FrameX360.exe`.

### Executar

- **Desenvolvimento:** `dotnet run --project XToolRGH.csproj`
- **Release:** executar `FrameX360.exe` a partir de `bin\Release\net48\`

---

## Estrutura do projeto

| Pasta / ficheiro | Descrição |
|------------------|-----------|
| `Patches\` | Ficheiros `.patch.toml` (um por jogo). O nome deve começar pelo **Title ID** (8 caracteres hex), ex: `534307F6 - Batman Arkham Asylum.patch.toml` |
| `xextool.exe` | Colocar na pasta do executável para decrypt/descompressão de XEX (opcional) |
| `lang.txt` | Criado automaticamente na pasta do exe; contém `en` ou `pt` para o idioma da interface |

---

## Formato dos patches

Os patches locais devem ser ficheiros **TOML** com o nome no formato:

```
<TitleID> - <Nome do jogo>.patch.toml
```

Exemplo: `5454082B - Red Dead Redemption (GOTY, Disc 1).patch.toml`

O formato é compatível com o usado no repositório [xenia-canary/game-patches](https://github.com/xenia-canary/game-patches).

---

## Utilização resumida

1. Abrir **FrameX360** e ir ao separador **FrameX**.
2. Clicar em **Selecionar** e escolher o ficheiro `.xex` do jogo.
3. O **Title ID** é lido automaticamente; os patches compatíveis aparecem na lista (locais e/ou do GitHub).
4. Se não houver correspondência, usar **Buscar patches** para pesquisar no GitHub por nome do jogo.
5. Selecionar os patches desejados e clicar em **Aplicar Patches** (é criado backup `.bak`).
6. Para XEX encriptado/comprimido, usar **Decrypt / Decompress** (requer `xextool.exe`).

---

## Licença

Conforme licença do repositório.
