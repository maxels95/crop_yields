import os
from git import Repo

# Path to the repository
repo_path = os.path.abspath(os.path.join(os.path.dirname(__file__), "../.."))

# Open the repository
repo = Repo(repo_path)

# Fetch the latest changes
origin = repo.remotes.origin
origin.fetch()

# Check if there are new commits to pull
if repo.head.commit != repo.refs['origin/main'].commit:
    print("New changes found. Pulling updates...")
    origin.pull()
else:
    print("No changes detected.")
