# Исследование БД ключ-значение, на примере memcached

### Теория:
+ https://memcached.org/about  
Memcache является универсальной системой распределения кэшированных элементов.  
Если в кэше ничего нет, то делается запрос к базе и результаты записываются в Memcache:  
Memcache предоставляет 5 функций:  
+ get() – извлекает значение по ключу  
+ set() – устанавливает значение  
+ add() – добавляет кэш, если его не существует  
+ replace() – заменяет кэш  
+ flush() – удаляет все закэшированные данные  

### Пример 1:
Используя memcached создать, удалить пару “ключ-значение” и проверить наличие “ключа”.
Пример создания и извлечения пары “ключ-значение” на языке python 3.10:
```python
from pymemcache.client import base
memc = base.Client(('localhost', 11211)) #коннект к Memcached
memc.set('key','value') #установка key-value
print(connect.get('key')) #вывод значения по ключу
memc.delete('key') #удаление ключа
```

### Пример 2:
Используя memcached создать сервис кеширования записей БД MySQL.
Пример создания кэша из СУБД на языке python:
```python
from pymemcache.client import base
import pymysql
import sys

try:
    con = pymysql.connect(host='localhost', #коннект к MySQL
                          user='root',
                          password='',
                          database='sakila')
    memc = base.Client(('localhost', 11211)) #коннект к Memcached
    cursor = con.cursor()
    cursor.execute('SELECT actor_id, first_name FROM actor LIMIT 50')#выполнение запроса
    rows = cursor.fetchall()#сохранение записей
    for row in rows:
        memc.set('%d' % row[0],'%s' % row[1]) #установка key-value

except pymysql.err.OperationalError as e:
    print("Error reading data from MySQL table", e)
    print("Error %d: %s" % (e.args[0], e.args[1]))
    sys.exit (1)

for i in range(50): #вывод key-value
    print('key: %s, value: %s' % (i, memc.get(str(i))))
```
 
+ Документация pymemcache: https://pymemcache.readthedocs.io/en/latest/ 
+ Описание pymysql: https://pypi.org/project/PyMySQL/ 
 
