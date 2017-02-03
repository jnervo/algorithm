# -*- coding:utf8 -*-
import sys
import codecs

f_in = codecs.open(sys.argv[1], 'r', 'utf8')
f_out = codecs.open(sys.argv[2], 'w','utf8')

for line in f_in:
    line = line.strip()
    if line == '':
        f_out.write('\n')
        continue
    msg = line.split()
    word = msg[0]
    sparse_featlst = msg[1:-3]
    postag = '[T1]' + msg[-3][5:]
    topictag = '[T2]' + msg[-2][5:]
    outputtag = msg[-1]
    
    sparse_feat=''
    for feat in sparse_featlst:
        sparse_feat += feat + ' '
    sparse_feat = sparse_feat.strip()
    
    char_feat = ''
    for char in word:
        char_feat += '[C]'+char+ ' '
    char_feat = char_feat.strip()
    outputmsg = word + ' '+char_feat + ' ' +sparse_feat + ' '+postag + ' '+outputtag
    f_out.write(outputmsg + '\n')
    
f_in.close()
f_out.close()