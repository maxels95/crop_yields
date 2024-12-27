import os
from git import Repo

# Path to the repository
repo_path = os.path.abspath(os.path.join(os.path.dirname(__file__), "../.."))

# Open the repository
repo = Repo(repo_path)

# Fetch the latest changes
upstream = repo.remotes.upstream
upstream.fetch()

# Check if there are new commits to pull
if repo.head.commit != repo.refs['upstream/main'].commit:
    print("New changes found. Pulling updates...")
    upstream.pull()
else:
    print("No changes detected.")
