# Разработка схемы данных графовой БД, на примере Neo4j

### Теория:
Neo4j – новый тип хранилищ данных NoSQL, получивший название графовая база данных.  
Как следует из названия, данные в ней хранятся в виде графа (в том смысле, в каком он рассматривается в математике). Отличительной особенностью таких баз данных является возможность рисовать их структуру на доске в виде прямоугольных блоков и соединяющих их линий. Всё, что можно изобразить в таком виде, можно сохранить в Neo4j. В Neo4j упор делается скорее на связи между значениями, чем на общие характеристики значений (как в коллекциях документов или в строках таблицы). Таким образом, совершенно разнородные данные можно хранить просто и естественно.
Размер Neo4j невелик – настолько, что ее можно внедрить практически в любое приложение. С другой стороны, в Neo4j можно хранить десятки миллиардов узлов и столько же ребер. А учитывая поддержку репликации главный-подчиненный на много серверов, эта СУБД способна справиться с задачей любого размера.  
+ Информация о Neo4j: https://neo4j.com/product/neo4j-graph-database/   
+ Cypher Manual: https://neo4j.com/docs/cypher-manual/current/ 

### Cypher guide

#### CREATE
Create a node  
```cypher
CREATE (ee:Person {name: 'Emil', from: 'Sweden', kloutScore: 99})  
```
• CREATE creates the node.
• () indicates the node.
• ee:Person – ee is the node variable andPerson is the node label.
• {} contains the properties that describe the node.

#### MATCH
Find nodes  
Now, find the node representing Emil.  
```cypher
MATCH (ee:Person) WHERE ee.name = 'Emil' RETURN ee;
```
+ MATCH specifies a pattern of nodes and relationships.
+ (ee:Person) is a single node pattern with label Person. It assigns matches to the variable ee.
+ WHERE filters the query.
+ ee.name = 'Emil' compares name property to the value Emil.
+ RETURN returns particular results.

#### CREATE more data
Nodes and relationships  

The CREATE clause can create many nodes and relationships at once.  

```cypher
MATCH (ee:Person) WHERE ee.name = 'Emil'
CREATE (js:Person { name: 'Johan', from: 'Sweden', learn: 'surfing' }),
(ir:Person { name: 'Ian', from: 'England', title: 'author' }),
(rvb:Person { name: 'Rik', from: 'Belgium', pet: 'Orval' }),
(ally:Person { name: 'Allison', from: 'California', hobby: 'surfing' }),
(ee)-[:KNOWS {since: 2001}]->(js),(ee)-[:KNOWS {rating: 5}]->(ir),
(js)-[:KNOWS]->(ir),(js)-[:KNOWS]->(rvb),
(ir)-[:KNOWS]->(js),(ir)-[:KNOWS]->(ally),
(rvb)-[:KNOWS]->(ally)
```

#### MATCH patterns
Describe what to find in the graph  
For instance, a pattern can be used to find Emil's friends:  
```cypher
MATCH (ee:Person)-[:KNOWS]-(friends)  
WHERE ee.name = 'Emil' RETURN ee, friends  
```
+ MATCH describes what nodes will be retrieved based upon the pattern.
+ (ee) is the node reference that will be returned based upon the WHERE clause.
+ -[:KNOWS]- matches the KNOWS relationships (in either direction) from ee.
+ (friends) represents the nodes that are Emil's friends.
+ RETURN returns the node, referenced here by (ee), and the related (friends) nodes found.

Python и Neo4j  
+ Драйвер Neo4j для python: https://pypi.org/project/neo4j/ 



### Пример:
```python
from neo4j import GraphDatabase
import pymysql
import sys


def actor_exist(tx, actor):
    answer = tx.run("MATCH (a:Actor {name: $actor_name}) RETURN (a.name)",
                    actor_name=actor)
    return answer.single()


def film_exist(tx, film):
    answer = tx.run("MATCH (a:Film {title: $film_title}) RETURN (a.title)",
                    film_title=film)
    return answer.single()


def create_actor(tx, actor):
    tx.run("CREATE (a:Actor {name: $actor_name})",
           actor_name=actor)


def create_film(tx, film):
    tx.run("CREATE (a:Film {title: $film_title})",
           film_title=film)


def create_actor_in_film(tx, actor, film):
    tx.run("MATCH (a:Actor), (b:Film) WHERE a.name = $actor_name AND b.title = $film_title "
           "CREATE (a)-[r:STAR_IN]->(b)",
           actor_name=actor, film_title=film)


driver = GraphDatabase.driver("neo4j://localhost:7687", auth=("neo4j", "1234"))

try:
    con = pymysql.connect(host='localhost', #коннект к MySQL
                          user='root',
                          password='',
                          database='sakila')
    cursor = con.cursor()
    cursor.execute('SELECT concat(first_name,\' \',last_name), title FROM actor JOIN film_actor USING(actor_id) JOIN film USING(film_id)')#выполнение запроса
    rows = cursor.fetchall()#сохранение записей
    for row in rows:
        with driver.session() as session:
            is_actor_exist = session.write_transaction(actor_exist, row[0])
            if is_actor_exist is None: #существует ли такой актёр
                session.write_transaction(create_actor, row[0])
            is_film_exist = session.write_transaction(film_exist, row[1])
            if is_film_exist is None: #существует ли такой фильм
                session.write_transaction(create_film, row[1])
            session.write_transaction(create_actor_in_film, row[0], row[1]) #создание связи актёр-фильм
            print('Create: %s - %s' % (row[0], row[1]))

except pymysql.err.OperationalError as e:
    print("Error reading data from MySQL table", e)
    print("Error %d: %s" % (e.args[0], e.args[1]))
    sys.exit (1)
```
