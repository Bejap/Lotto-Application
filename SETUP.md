# Repository Setup

## Branches Created

This repository has been set up with the following branches:

1. **main** - The main production branch
2. **develop** - The development branch for ongoing work

Both branches have been created locally with the updated README that includes a comprehensive description of the Lotto Application.

## Branch Structure

The recommended Git workflow for this repository is:

- **main**: Production-ready code. All releases should be tagged from this branch.
- **develop**: Integration branch for features. All feature branches should be merged here first.
- **feature branches**: Created from `develop` for new features, merged back to `develop` when complete.

## Next Steps

To push these branches to the remote repository, run:

```bash
git push -u origin main
git push -u origin develop
```

## Setting Default Branch

After pushing, you may want to set `main` as the default branch in GitHub:

1. Go to repository Settings
2. Navigate to Branches
3. Set "Default branch" to `main`

## Branch Protection (Recommended)

Consider setting up branch protection rules for both `main` and `develop` branches:

1. Require pull request reviews before merging
2. Require status checks to pass
3. Include administrators in restrictions
