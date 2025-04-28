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

Toutes les fonctionnalités de l'énoncé sont implémentées.
- [x] Authentification
- [x] Inscription
- [x] Gestion des colonies
- [x] Gestion des ressources
- [x] Gestion des missions
- [x] Gestion des utilisateurs
- [x] Statistiques
- [x] Boutique Vente/Achat
- [x] Lancement de missions

## Données de connexion
- Administrateur : 
  - Login : `admin`
  - Mot de passe : `Password123_`
- Utilisateur :
  - Login : `JettRaven`
  - Mot de passe : `Player123_`

## Éléments techniques notables

### Tests automatisés
Aucun tests n'a été implémenté dans le projet.