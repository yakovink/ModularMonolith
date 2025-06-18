#!/bin/bash
PATH="src/Modules/$1"
cd "$PATH" || { echo "Directory $PATH does not exist."; exit 1; }
# Set files
OUTPUT_FILE="./GlobalUsing.cs"
TEMP_FILE="./_new_usings.tmp"
ALL_USINGS="./_all_usings.tmp"

# Extract all current global usings in GlobalUsing.cs
grep -E "^global using [a-zA-Z0-9_.]+;" "$OUTPUT_FILE" 2>/dev/null | sort | uniq > "$ALL_USINGS"

# Find and extract local using statements from all .cs files (excluding bin/obj/GlobalUsing.cs itself)
find . -type f -name "*.cs" \
  ! -path "./obj/*" \
  ! -path "./bin/*" \
  ! -name "GlobalUsing.cs" | while read -r file; do
    grep -E "^using\s+[a-zA-Z0-9_.]+;" "$file" >> "$TEMP_FILE"
done

# Normalize: convert to global using, sort, deduplicate
cat "$TEMP_FILE" | sed 's/^using /global using /' | sort | uniq > "$TEMP_FILE.sorted"

# Append only new global usings
comm -23 "$TEMP_FILE.sorted" "$ALL_USINGS" >> "$OUTPUT_FILE"

# Cleanup
rm -f "$TEMP_FILE" "$TEMP_FILE.sorted" "$ALL_USINGS"

# Done
echo "Appended new global usings to $OUTPUT_FILE."
# Exit successfully
cd ../../..
