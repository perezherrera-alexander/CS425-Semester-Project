import os

# Get the current folder path
folder_path = os.getcwd()

# Iterate over each file in the folder
for filename in os.listdir(folder_path):
    # Check if the file is a regular file
    if os.path.isfile(os.path.join(folder_path, filename)):
        # Remove the first 20 letters from the filename
        new_filename = filename[1:]
        
        # Rename the file
        os.rename(os.path.join(folder_path, filename), os.path.join(folder_path, new_filename))