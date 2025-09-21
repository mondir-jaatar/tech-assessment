# Conception et architecture de système

L'application dans son état pour le test technique n'est pas utilisable en production.

Faites une liste de l'ensemble des éléments que vous estimez devoir être corrigés et/ou améliorés pour avoir une application "prod-ready".
Pour chaque élément, proposez également un plan de remédiation.

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