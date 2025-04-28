# Star Colonies

Projet d'application web pour l'UE19. Le projet prend la forme d'une solution .NET découpée en trois projets :

- un projet d'application web Razor Page
- un projet de bibliothèque modélisant le domaine.
- un projet d'infrastructures, ce dernier reprenant notamment les éléments propres à EntityFrameworkCore.

La suite du document sera à compléter par vos soins.

## Membres de l'équipe

- Boukhanouche Ayoub(*Q230049*)
- Ciasiolki Alexandre(*Q230119*)
- Aerts Sacha()


- **Version Production** : [Q230049](https://ue19.cg.helmo.be/q230049)

## Construction de la solution

### Technologies utilisées :
- ASP.NET Core Razor Pages
- Entity Framework Core
- .NET 8

### Etapes de récupération
1. Cloner le dépôt
```bash
git clone https://git.helmo.be/students/info/q230049/star-colonies
```

## Fonctionnalités implémentées

**TODO :** pour chaque US décrite dans l'énoncé, indiquez son état d'avancement (non-faite, débutée, partiellement achevée, totalement achevée). Quand une US est débutée ou partiellement achevée, indiquez en quelques mots ce qui manque selon vous.

## Données de connexion

**TODO :** indiquez au minimum trois comptes dont un compte d'administrateur.

## Éléments techniques notables

### Tests automatisés
Des **tests d’intégration** ont été mis en place pour valider la protection anti-DoS.

**Middleware testé :** 
 - `RateLimitingMiddleware` (limite chaque adresse ip à 10 requêtes toutes les 10 secondes)
    - Librairie utilisée : 
      - [Microsoft.AspNetCore.Mvc.Testing](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing)
      - [xUnit](https://xunit.net/)

```csproj
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="8.0.0" />
```