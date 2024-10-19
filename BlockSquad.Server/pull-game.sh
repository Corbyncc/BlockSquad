#!/bin/bash

# Get the latest successful workflow run
RUN_ID=$(curl -s -H "Authorization: token $GitHubToken" \
  "https://api.github.com/repos/corbyncc/blocksquad/actions/workflows/build-game.yml/runs?status=success&per_page=1" |
  jq -r '.workflow_runs[0].id')

# Get the artifact ID
ARTIFACT_ID=$(curl -s -H "Authorization: token $GitHubToken" \
  "https://api.github.com/repos/corbyncc/blocksquad/actions/runs/$RUN_ID/artifacts" |
  jq -r '.artifacts[0].id')

# Download the artifact
curl -L -H "Authorization: token $GitHubToken" \
  -o artifact.zip \
  "https://api.github.com/repos/corbyncc/blocksquad/actions/artifacts/$ARTIFACT_ID/zip"

echo "Artifact downloaded: artifact.zip"
