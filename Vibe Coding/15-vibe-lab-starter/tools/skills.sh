#!/usr/bin/env bash
#
# skills.sh -- find and install real Claude Code agent skills
# -------------------------------------------------------------
# Part of the ENVNT intern program (program/3-vibe-coding/tools/).
# This is Day 11's Pillar-1 (Skills) helper tool.
#
# What it does:
#   1. Explains how Claude Code's skill system actually works (no
#      extra install, no account, no hidden magic).
#   2. Scans THIS machine for skills you already have.
#   3. Prints a short, curated starter list -- with the exact command
#      to get each one.
#   4. Reminds you of the fastest path of all: the built-in find-skills
#      flow, where you just ask Claude Code in plain language.
#
# How to run:
#   bash tools/skills.sh              full walkthrough (all sections below)
#   bash tools/skills.sh --installed  only scan what you already have
#   bash tools/skills.sh --list       only print the curated starter list
#   bash tools/skills.sh --help       usage
#
# This script is READ-ONLY. It never downloads or installs anything for
# you -- every command it prints is one YOU run, on purpose, after you've
# read what it does. That's the least-privilege habit from Lab 1, Drill C.

set -eu

# ---------------------------------------------------------------------------
# Curated starter list -- name | one-line description | how to get it.
# Keep this short and honest: only list skills you've actually seen work.
# Add a row here (same three-array shape) once you've tried a new one.
# ---------------------------------------------------------------------------
SKILL_NAMES=(
  "frontend-design"
  "pdf"
  "docx"
  "pptx"
  "xlsx"
  "skill-creator"
)
SKILL_DESCS=(
  "Distinctive, production-grade UI work: components, pages, dashboards."
  "Read, fill, merge, split, or watermark PDF files."
  "Create, read, or edit Word (.docx) documents."
  "Build or edit PowerPoint (.pptx) presentations."
  "Read, edit, or generate Excel/CSV spreadsheets."
  "Scaffold a brand-new skill of your own, the right way."
)
SKILL_SOURCES=(
  "often bundled already, or: npx skills add frontend-design"
  "often bundled already (document skills), or: npx skills add pdf"
  "often bundled already (document skills), or: npx skills add docx"
  "often bundled already (document skills), or: npx skills add pptx"
  "often bundled already (document skills), or: npx skills add xlsx"
  "npx skills add skill-creator"
)

print_header() {
  echo "=================================================="
  echo " skills.sh -- your Claude Code skill toolbox"
  echo "=================================================="
  echo
}

print_how_skills_work() {
  cat <<'EOF'
HOW SKILLS ACTUALLY WORK (no magic, no extra install)
-------------------------------------------------------
A skill is just a folder with one required file: SKILL.md

  - YAML frontmatter gives it a "name" and a "description."
  - The description is what makes it AUTO-TRIGGER: Claude Code reads
    every installed skill's description and loads the matching one on
    its own when your request fits.
  - It can also be called on demand: /<skill-name>

Skills live in one of two places:
  ~/.claude/skills/<name>/SKILL.md   personal -- every project, this machine
  .claude/skills/<name>/SKILL.md     project  -- committed to git, ships with the repo

That's the whole mechanism. Nothing else is required to use one.

EOF
}

scan_installed() {
  echo "WHAT YOU ALREADY HAVE"
  echo "------------------------"
  found_any=0
  for dir in "$HOME/.claude/skills" "./.claude/skills"; do
    if [ -d "$dir" ]; then
      matches=$(find "$dir" -maxdepth 2 -iname "SKILL.md" 2>/dev/null || true)
      if [ -n "$matches" ]; then
        echo "In $dir:"
        while IFS= read -r f; do
          [ -z "$f" ] && continue
          name=$(basename "$(dirname "$f")")
          echo "  - $name"
          found_any=1
        done <<EOF2
$matches
EOF2
      fi
    fi
  done
  if [ "$found_any" -eq 0 ]; then
    echo "  (none found in ~/.claude/skills or ./.claude/skills yet -- that's normal on Day 11)"
  fi
  echo
}

print_curated_list() {
  echo "A SHORT STARTER LIST (curated, not exhaustive)"
  echo "--------------------------------------------------"
  i=0
  count=${#SKILL_NAMES[@]}
  while [ "$i" -lt "$count" ]; do
    printf "  %-16s %s\n" "${SKILL_NAMES[$i]}" "${SKILL_DESCS[$i]}"
    printf "  %-16s -> %s\n\n" "" "${SKILL_SOURCES[$i]}"
    i=$((i + 1))
  done
}

print_find_skills_flow() {
  cat <<'EOF'
THE FASTEST PATH: JUST ASK (the find-skills flow)
-----------------------------------------------------
You don't have to browse a list by hand. Inside Claude Code, ask in plain
language:

  "is there a skill for building a PDF report?"
  "find me a skill for reviewing a pull request"

Claude Code's built-in find-skills skill searches the open skills
ecosystem for you -- including the community registry at skills.sh --
checks install counts and source reputation, and hands you the exact
install command. That IS Pillar 1: you equipped the AI with a tool for
finding tools.

Prefer to browse yourself? The registry is at https://skills.sh and its
CLI is `npx skills` (e.g. `npx skills add <name>`).
EOF
  echo
}

print_least_privilege_reminder() {
  cat <<'EOF'
BEFORE YOU INSTALL ANYTHING (Lab 1, Drill C)
-------------------------------------------------
Ask three questions, out loud, every time:
  1. Does this task need read-only, or read AND write?
  2. What's the worst thing this skill could do if misread, or if it was
     badly written to begin with?
  3. Could anything it reads or fetches contain hidden instructions
     (prompt injection)? What would you do about it?

If it can write to something that matters, keep a human approval step in
the loop.
EOF
}

usage() {
  echo "Usage: bash tools/skills.sh [--installed|--list|--help]"
}

main() {
  case "${1:-}" in
    --help|-h)
      usage
      ;;
    --installed)
      print_header
      scan_installed
      ;;
    --list)
      print_header
      print_curated_list
      ;;
    "")
      print_header
      print_how_skills_work
      scan_installed
      print_curated_list
      print_find_skills_flow
      print_least_privilege_reminder
      ;;
    *)
      echo "Unknown option: $1" >&2
      usage >&2
      exit 1
      ;;
  esac
}

main "$@"
