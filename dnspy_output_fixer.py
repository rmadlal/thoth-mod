import os

changes = 0

def fix_file(filename):
    with open(filename) as file:
        contents = file.readlines()
    contents = [fix(line) for line in contents]
    with open(filename, 'w') as file:
        file.writelines(contents)


def fix(line):
    global changes
    if line.strip().startswith('//') and 'Token:' in line:
        changes += 1
        return ''
    mod_line = line
    while '.get_Item' in mod_line:
        head, sep, tail = mod_line.partition('.get_Item')
        closing_paren = find_closing_paren(tail)
        tail = tail[:closing_paren] + ']' + tail[closing_paren + 1:]
        tail = tail.replace('(', '[', 1)
        mod_line = head + tail
    while 'get_' in mod_line:
        head, sep, tail = mod_line.partition('get_')
        tail = tail.replace('()', '', 1)
        mod_line = head + tail
    while 'set_' in mod_line:
        head, sep, tail = mod_line.partition('set_')
        closing_paren = find_closing_paren(tail)
        tail = tail[:closing_paren] + tail[closing_paren + 1:]
        tail = tail.replace('(', ' = ', 1)
        mod_line = head + tail
    if mod_line != line:
        changes += 1
    return mod_line


def find_closing_paren(s):
    count = 0
    for i, c in enumerate(s):
        if c == '(':
            count += 1
        elif c == ')':
            count -= 1
            if not count:
                return i
    return -1


for file in os.listdir():
    filename = os.fsdecode(file)
    if filename.endswith(".cs"): 
        fix_file(filename)
print('total lines changed: ' + str(changes))
