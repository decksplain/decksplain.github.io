## Local development

1. Use devcontainers and in the terminal run:
    - `bundle install`
    - `jekyll serve --verbose --force_polling`
        - `--verbose` helps with seeing that changes have really been picked up
        - `--force_polling` seems to solve an issue (maybe with VSCode in dev containers?) where some files (such as scss files) aren't watched properly
2. Visit the site at http://localhost:4000
