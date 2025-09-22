# Éléments à corriger/améliorer pour rendre l’application "prod-ready"

## 1. Sécurité
- **Problème** :
    - Actuellement, les tokens sont stockés dans `localStorage`, ce qui est vulnérable aux attaques XSS.
    - L’authentification repose sur un mécanisme simplifié qui n’est pas adapté à une application en production.
- **Remédiation** :
    - Mettre en place une authentification basée sur **Duende IdentityServer**.
    - Utiliser le **Authorization Code Flow avec PKCE** pour échanger les tokens (plutôt que cookies).
    - Passer à une connexion via **JWT (JSON Web Tokens)** pour l’authentification et l’autorisation.

## 2. Concurrence et intégrité des données
- **Problème** :
    - En cas d’inscriptions simultanées à une même session, il existe un risque de **surbooking** (plus d’utilisateurs inscrits que de places disponibles).
- **Remédiation** :
    - Mettre en place un **mécanisme de verrouillage** (par exemple un **semaphore** ou un **lock distribué via Redis**) pour garantir qu’une seule transaction puisse réserver une place à la fois.
    - Un service a déjà été développé **RedisLockService**
    - Utiliser des **contrôle d’intégrité côté base de données** (par ex. contraintes uniques, vérification du stock de places avant validation).

## 3. Performance
- **Problème** :
    - Certaines requêtes chargent plus de données que nécessaire (absence de pagination / filtres).
    - Les composants React chargent parfois trop de code inutile au démarrage (bundle lourd).
- **Remédiation** :
    - Mettre en place la **pagination et le filtrage** côté API pour éviter les requêtes trop volumineuses.
    - Activer le **lazy loading** et le **code splitting** côté front (par ex. avec `React.lazy` et `Suspense`) pour ne charger que les pages et composants nécessaires.
    - Utiliser un **CDN** pour les assets statiques (images, fonts, etc.).

## 4. Tests et qualité
- **Problème** : Pas de tests unitaires, ni end-to-end.
- **Remédiation** :
    - Ajouter des tests unitaires.
    - Écrire des tests E2E (puppeteer).
    - Configurer une couverture minimale dans le pipeline CI.

## 5. Industrialisation et déploiement
- **Problème** : Pas de pipeline de CI/CD ni de monitoring en place.
- **Remédiation** :
    - Configurer une CI/CD (GitHub Actions) pour build + tests + déploiement auto.
    - Ajouter un monitoring (logs, métriques, alertes).
    - Prévoir des environnements distincts (dev, staging, prod).

## 6. Observabilité et logs
- **Problème** : Pas de traçage ni de logs côté client/serveur.
- **Remédiation** :
    - Ajouter des logs structurés côté backend.
    - Mettre en place un outil de suivi des erreurs frontend (Seq).

========
## Proposition : Validation par email

### 1. Flux d’interaction utilisateur

#### a. Consultation des sessions disponibles
1. L’utilisateur accède à la page publique listant les sessions de formation (dates, lieux, places disponibles).
2. Il sélectionne la session souhaitée et clique sur **“S’inscrire”**.

#### b. Remplissage du formulaire d’inscription
1. L’utilisateur saisit ses informations principales :
    - Nom et prénom
    - Adresse email
    - Entreprise
2. Il soumet le formulaire.

#### c. Validation de l’inscription
1. Le système génère un **lien unique de confirmation** (token sécurisé, limité dans le temps).
2. L’utilisateur reçoit un **email de confirmation** contenant ce lien.
3. Il doit cliquer sur le lien pour valider son inscription.

### 2. Mécanismes de sécurité
- **Double validation par email** pour éviter les inscriptions au nom d’un tiers.
- **Jetons de confirmation uniques et expirants** pour éviter les abus.
- **Rate limiting et CAPTCHA** pour prévenir les scripts automatisés.