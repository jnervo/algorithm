import MySQLdb

conn=MySQLdb.connect(host='127.0.0.1',port=3306,user='root',passwd='1234',db='bbs',charset='utf8')

cursor=conn.cursor()

for i in range(1,10):
    cursor.execute("insert into user(iduser,name) values('%d','%s')" %(int(i),'name'+str(i)))
conn.commit()


cursor.close()
conn.close()