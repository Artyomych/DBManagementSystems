# Разработка приложений с использованием NOSQL баз данных, на примере MongoDB (v4.4)

### Теория:
Документация:
+ https://docs.mongodb.com/v4.4/introduction/ (En) 
+ https://metanit.com/nosql/mongodb/ (Rus)

Загрузить:
+ https://www.mongodb.com/try/download/community  

Хороший гайд на хабре:  
+ https://habr.com/ru/post/580760/  

MongoDB — документоориентированная система управления базами данных, не требующая описания схемы таблиц.
Считается одним из классических примеров NoSQL-систем, использует JSON-подобные документы и схему базы данных. Написана на языке C++.


### Пример:
Создать сервис, перемещающий данные о фильмах из таблицы film БД sakila в mongodb.  
Формат документа:
```json
{
"film_id": "идентификатор фильма",
"title": "название",
"description": "описание",
"rating": "рейтинг"
}
```

### Пример решения на языке Python

```python
from pymongo import MongoClient
import pymysql
import sys

#Step 1: Connect to MongoDB - Note: Change connection string as needed
client = MongoClient(port=27017)
db=client.sakila
try:
    con = pymysql.connect(host='localhost', #коннект к MySQL
                          user='root',
                          password='',
                          database='sakila')
    cursor = con.cursor()
    # Step 2: Create sample data
    cursor.execute('SELECT film_id,title,description,rating FROM film')#выполнение запроса
    rows = cursor.fetchall()#сохранение записей
    for row in rows:
        film = {
            'film_id': row[0],
            'title': row[1],
            'description': row[2],
            'rating': row[3]
        }
        # Step 3: Insert sakila.films object directly into MongoDB via insert_one
        result = db.films.insert_one(film)
        # Step 4: Print to the console the ObjectID of the new document
        print('Created film {0} with inserted id: {1}'.format(row[1], result.inserted_id))


except pymysql.err.OperationalError as e:
    print("Error reading data from MySQL table", e)
    print("Error %d: %s" % (e.args[0], e.args[1]))
    sys.exit(1)
```
 
### Средствами MongoDB найти фильмы с рейтингом G
Команда для mongodb:  
>db.films.find({rating:{$eq:"G"}}).pretty()  

#### Комментарий
Условные операторы задают условие, которому должно соответствовать значение поля документа:  
+ $eq (равно)
+ $ne (не равно)
+ $gt (больше чем)
+ $lt (меньше чем)
+ $gte (больше или равно)
+ $lte (меньше или равно)
+ $in определяет массив значений, одно из которых должно иметь поле документа
+ $nin определяет массив значений, которые не должно иметь поле документа

Для структурированного вывода данных коллекции используется метод ```pretty()```

Результат (часть):  
```json
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585637"),
        "film_id" : 2,
        "title" : "ACE GOLDFINGER",
        "description" : "A Astounding Epistle of a Database Administrator And a Explorer who must Find a Car in Ancient China",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585639"),
        "film_id" : 4,
        "title" : "AFFAIR PREJUDICE",
        "description" : "A Fanciful Documentary of a Frisbee And a Lumberjack who must Chase a Monkey in A Shark Tank",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58563a"),
        "film_id" : 5,
        "title" : "AFRICAN EGG",
        "description" : "A Fast-Paced Documentary of a Pastry Chef And a Dentist who must Pursue a Forensic Psychologist in The Gulf of Mexico",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585640"),
        "film_id" : 11,
        "title" : "ALAMO VIDEOTAPE",
        "description" : "A Boring Epistle of a Butler And a Cat who must Fight a Pastry Chef in A MySQL Convention",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58564b"),
        "film_id" : 22,
        "title" : "AMISTAD MIDSUMMER",
        "description" : "A Emotional Character Study of a Dentist And a Crocodile who must Meet a Sumo Wrestler in California",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58564e"),
        "film_id" : 25,
        "title" : "ANGELS LIFE",
        "description" : "A Thoughtful Display of a Woman And a Astronaut who must Battle a Robot in Berlin",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58564f"),
        "film_id" : 26,
        "title" : "ANNIE IDENTITY",
        "description" : "A Amazing Panorama of a Pastry Chef And a Boat who must Escape a Woman in An Abandoned Amusement Park",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58565c"),
        "film_id" : 39,
        "title" : "ARMAGEDDON LOST",
        "description" : "A Fast-Paced Tale of a Boat And a Teacher who must Succumb a Composer in An Abandoned Mine Shaft",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585660"),
        "film_id" : 43,
        "title" : "ATLANTIS CAUSE",
        "description" : "A Thrilling Yarn of a Feminist And a Hunter who must Fight a Technical Writer in A Shark Tank",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585663"),
        "film_id" : 46,
        "title" : "AUTUMN CROW",
        "description" : "A Beautiful Tale of a Dentist And a Mad Cow who must Battle a Moose in The Sahara Desert",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585667"),
        "film_id" : 50,
        "title" : "BAKED CLEOPATRA",
        "description" : "A Stunning Drama of a Forensic Psychologist And a Husband who must Overcome a Waitress in A Monastery",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585669"),
        "film_id" : 52,
        "title" : "BALLROOM MOCKINGBIRD",
        "description" : "A Thrilling Documentary of a Composer And a Monkey who must Find a Feminist in California",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58566c"),
        "film_id" : 55,
        "title" : "BARBARELLA STREETCAR",
        "description" : "A Awe-Inspiring Story of a Feminist And a Cat who must Conquer a Dog in A Monastery",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58566d"),
        "film_id" : 56,
        "title" : "BAREFOOT MANCHURIAN",
        "description" : "A Intrepid Story of a Cat And a Student who must Vanquish a Girl in An Abandoned Amusement Park",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58566f"),
        "film_id" : 58,
        "title" : "BEACH HEARTBREAKERS",
        "description" : "A Fateful Display of a Womanizer And a Mad Scientist who must Outgun a A Shark in Soviet Georgia",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585672"),
        "film_id" : 61,
        "title" : "BEAUTY GREASE",
        "description" : "A Fast-Paced Display of a Composer And a Moose who must Sink a Robot in An Abandoned Mine Shaft",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585680"),
        "film_id" : 75,
        "title" : "BIRD INDEPENDENCE",
        "description" : "A Thrilling Documentary of a Car And a Student who must Sink a Hunter in The Canadian Rockies",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585682"),
        "film_id" : 77,
        "title" : "BIRDS PERDITION",
        "description" : "A Boring Story of a Womanizer And a Pioneer who must Face a Dog in California",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585685"),
        "film_id" : 80,
        "title" : "BLANKET BEVERLY",
        "description" : "A Emotional Documentary of a Student And a Girl who must Build a Boat in Nigeria",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585687"),
        "film_id" : 82,
        "title" : "BLOOD ARGONAUTS",
        "description" : "A Boring Drama of a Explorer And a Man who must Kill a Lumberjack in A Manhattan Penthouse",
        "rating" : "G"
}
```

### Средствами MongoDB найти фильмы, идентификаторы которых больше 10 и меньше 50
Команда для mongodb:  
>db.films.find({$and: [{film_id:{$gt:10}}, {film_id:{$lt:50}}]}).pretty()  

Результат:  
```json
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585640"),
        "film_id" : 11,
        "title" : "ALAMO VIDEOTAPE",
        "description" : "A Boring Epistle of a Butler And a Cat who must Fight a Pastry Chef in A MySQL Convention",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585641"),
        "film_id" : 12,
        "title" : "ALASKA PHANTOM",
        "description" : "A Fanciful Saga of a Hunter And a Pastry Chef who must Vanquish a Boy in Australia",
        "rating" : "PG"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585642"),
        "film_id" : 13,
        "title" : "ALI FOREVER",
        "description" : "A Action-Packed Drama of a Dentist And a Crocodile who must Battle a Feminist in The Canadian Rockies",
        "rating" : "PG"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585643"),
        "film_id" : 14,
        "title" : "ALICE FANTASIA",
        "description" : "A Emotional Drama of a A Shark And a Database Administrator who must Vanquish a Pioneer in Soviet Georgia",
        "rating" : "NC-17"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585644"),
        "film_id" : 15,
        "title" : "ALIEN CENTER",
        "description" : "A Brilliant Drama of a Cat And a Mad Scientist who must Battle a Feminist in A MySQL Convention",
        "rating" : "NC-17"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585645"),
        "film_id" : 16,
        "title" : "ALLEY EVOLUTION",
        "description" : "A Fast-Paced Drama of a Robot And a Composer who must Battle a Astronaut in New Orleans",
        "rating" : "NC-17"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585646"),
        "film_id" : 17,
        "title" : "ALONE TRIP",
        "description" : "A Fast-Paced Character Study of a Composer And a Dog who must Outgun a Boat in An Abandoned Fun House",
        "rating" : "R"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585647"),
        "film_id" : 18,
        "title" : "ALTER VICTORY",
        "description" : "A Thoughtful Drama of a Composer And a Feminist who must Meet a Secret Agent in The Canadian Rockies",
        "rating" : "PG-13"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585648"),
        "film_id" : 19,
        "title" : "AMADEUS HOLY",
        "description" : "A Emotional Display of a Pioneer And a Technical Writer who must Battle a Man in A Baloon",
        "rating" : "PG"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585649"),
        "film_id" : 20,
        "title" : "AMELIE HELLFIGHTERS",
        "description" : "A Boring Drama of a Woman And a Squirrel who must Conquer a Student in A Baloon",
        "rating" : "R"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58564a"),
        "film_id" : 21,
        "title" : "AMERICAN CIRCUS",
        "description" : "A Insightful Drama of a Girl And a Astronaut who must Face a Database Administrator in A Shark Tank",
        "rating" : "R"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58564b"),
        "film_id" : 22,
        "title" : "AMISTAD MIDSUMMER",
        "description" : "A Emotional Character Study of a Dentist And a Crocodile who must Meet a Sumo Wrestler in California",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58564c"),
        "film_id" : 23,
        "title" : "ANACONDA CONFESSIONS",
        "description" : "A Lacklusture Display of a Dentist And a Dentist who must Fight a Girl in Australia",
        "rating" : "R"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58564d"),
        "film_id" : 24,
        "title" : "ANALYZE HOOSIERS",
        "description" : "A Thoughtful Display of a Explorer And a Pastry Chef who must Overcome a Feminist in The Sahara Desert",
        "rating" : "R"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58564e"),
        "film_id" : 25,
        "title" : "ANGELS LIFE",
        "description" : "A Thoughtful Display of a Woman And a Astronaut who must Battle a Robot in Berlin",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58564f"),
        "film_id" : 26,
        "title" : "ANNIE IDENTITY",
        "description" : "A Amazing Panorama of a Pastry Chef And a Boat who must Escape a Woman in An Abandoned Amusement Park",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585650"),
        "film_id" : 27,
        "title" : "ANONYMOUS HUMAN",
        "description" : "A Amazing Reflection of a Database Administrator And a Astronaut who must Outrace a Database Administrator in A Shark Tank",
        "rating" : "NC-17"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585651"),
        "film_id" : 28,
        "title" : "ANTHEM LUKE",
        "description" : "A Touching Panorama of a Waitress And a Woman who must Outrace a Dog in An Abandoned Amusement Park",
        "rating" : "PG-13"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585652"),
        "film_id" : 29,
        "title" : "ANTITRUST TOMATOES",
        "description" : "A Fateful Yarn of a Womanizer And a Feminist who must Succumb a Database Administrator in Ancient India",
        "rating" : "NC-17"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585653"),
        "film_id" : 30,
        "title" : "ANYTHING SAVANNAH",
        "description" : "A Epic Story of a Pastry Chef And a Woman who must Chase a Feminist in An Abandoned Fun House",
        "rating" : "R"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585654"),
        "film_id" : 31,
        "title" : "APACHE DIVINE",
        "description" : "A Awe-Inspiring Reflection of a Pastry Chef And a Teacher who must Overcome a Sumo Wrestler in A U-Boat",
        "rating" : "NC-17"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585655"),
        "film_id" : 32,
        "title" : "APOCALYPSE FLAMINGOS",
        "description" : "A Astounding Story of a Dog And a Squirrel who must Defeat a Woman in An Abandoned Amusement Park",
        "rating" : "R"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585656"),
        "film_id" : 33,
        "title" : "APOLLO TEEN",
        "description" : "A Action-Packed Reflection of a Crocodile And a Explorer who must Find a Sumo Wrestler in An Abandoned Mine Shaft",
        "rating" : "PG-13"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585657"),
        "film_id" : 34,
        "title" : "ARABIA DOGMA",
        "description" : "A Touching Epistle of a Madman And a Mad Cow who must Defeat a Student in Nigeria",
        "rating" : "NC-17"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585658"),
        "film_id" : 35,
        "title" : "ARACHNOPHOBIA ROLLERCOASTER",
        "description" : "A Action-Packed Reflection of a Pastry Chef And a Composer who must Discover a Mad Scientist in The First Manned Space Station",
        "rating" : "PG-13"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585659"),
        "film_id" : 36,
        "title" : "ARGONAUTS TOWN",
        "description" : "A Emotional Epistle of a Forensic Psychologist And a Butler who must Challenge a Waitress in An Abandoned Mine Shaft",
        "rating" : "PG-13"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58565a"),
        "film_id" : 37,
        "title" : "ARIZONA BANG",
        "description" : "A Brilliant Panorama of a Mad Scientist And a Mad Cow who must Meet a Pioneer in A Monastery",
        "rating" : "PG"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58565b"),
        "film_id" : 38,
        "title" : "ARK RIDGEMONT",
        "description" : "A Beautiful Yarn of a Pioneer And a Monkey who must Pursue a Explorer in The Sahara Desert",
        "rating" : "NC-17"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58565c"),
        "film_id" : 39,
        "title" : "ARMAGEDDON LOST",
        "description" : "A Fast-Paced Tale of a Boat And a Teacher who must Succumb a Composer in An Abandoned Mine Shaft",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58565d"),
        "film_id" : 40,
        "title" : "ARMY FLINTSTONES",
        "description" : "A Boring Saga of a Database Administrator And a Womanizer who must Battle a Waitress in Nigeria",
        "rating" : "R"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58565e"),
        "film_id" : 41,
        "title" : "ARSENIC INDEPENDENCE",
        "description" : "A Fanciful Documentary of a Mad Cow And a Womanizer who must Find a Dentist in Berlin",
        "rating" : "PG"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58565f"),
        "film_id" : 42,
        "title" : "ARTIST COLDBLOODED",
        "description" : "A Stunning Reflection of a Robot And a Moose who must Challenge a Woman in California",
        "rating" : "NC-17"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585660"),
        "film_id" : 43,
        "title" : "ATLANTIS CAUSE",
        "description" : "A Thrilling Yarn of a Feminist And a Hunter who must Fight a Technical Writer in A Shark Tank",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585661"),
        "film_id" : 44,
        "title" : "ATTACKS HATE",
        "description" : "A Fast-Paced Panorama of a Technical Writer And a Mad Scientist who must Find a Feminist in An Abandoned Mine Shaft",
        "rating" : "PG-13"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585662"),
        "film_id" : 45,
        "title" : "ATTRACTION NEWTON",
        "description" : "A Astounding Panorama of a Composer And a Frisbee who must Reach a Husband in Ancient Japan",
        "rating" : "PG-13"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585663"),
        "film_id" : 46,
        "title" : "AUTUMN CROW",
        "description" : "A Beautiful Tale of a Dentist And a Mad Cow who must Battle a Moose in The Sahara Desert",
        "rating" : "G"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585664"),
        "film_id" : 47,
        "title" : "BABY HALL",
        "description" : "A Boring Character Study of a A Shark And a Girl who must Outrace a Feminist in An Abandoned Mine Shaft",
        "rating" : "NC-17"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585665"),
        "film_id" : 48,
        "title" : "BACKLASH UNDEFEATED",
        "description" : "A Stunning Character Study of a Mad Scientist And a Mad Cow who must Kill a Car in A Monastery",
        "rating" : "PG-13"
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585666"),
        "film_id" : 49,
        "title" : "BADMAN DAWN",
        "description" : "A Emotional Panorama of a Pioneer And a Composer who must Escape a Mad Scientist in A Jet Boat",
        "rating" : "R"
}
```

### Добавление нового поля

Добавим поле ```price``` со значением 100 к каждому объекту  
>db.films.updateMany({},{$set:{price:100}})

Выведем объекты от 1 до 9  
>db.films.find({$and: [{film_id:{$gt:0}}, {film_id:{$lt:10}}]}).pretty()   

Получим  
```json
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585636"),
        "film_id" : 1,
        "title" : "ACADEMY DINOSAUR",
        "description" : "A Epic Drama of a Feminist And a Mad Scientist who must Battle a Teacher in The Canadian Rockies",
        "rating" : "PG",
        "price" : 100
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585637"),
        "film_id" : 2,
        "title" : "ACE GOLDFINGER",
        "description" : "A Astounding Epistle of a Database Administrator And a Explorer who must Find a Car in Ancient China",
        "rating" : "G",
        "price" : 100
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585638"),
        "film_id" : 3,
        "title" : "ADAPTATION HOLES",
        "description" : "A Astounding Reflection of a Lumberjack And a Car who must Sink a Lumberjack in A Baloon Factory",
        "rating" : "NC-17",
        "price" : 100
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f585639"),
        "film_id" : 4,
        "title" : "AFFAIR PREJUDICE",
        "description" : "A Fanciful Documentary of a Frisbee And a Lumberjack who must Chase a Monkey in A Shark Tank",
        "rating" : "G",
        "price" : 100
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58563a"),
        "film_id" : 5,
        "title" : "AFRICAN EGG",
        "description" : "A Fast-Paced Documentary of a Pastry Chef And a Dentist who must Pursue a Forensic Psychologist in The Gulf of Mexico",
        "rating" : "G",
        "price" : 100
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58563b"),
        "film_id" : 6,
        "title" : "AGENT TRUMAN",
        "description" : "A Intrepid Panorama of a Robot And a Boy who must Escape a Sumo Wrestler in Ancient China",
        "rating" : "PG",
        "price" : 100
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58563c"),
        "film_id" : 7,
        "title" : "AIRPLANE SIERRA",
        "description" : "A Touching Saga of a Hunter And a Butler who must Discover a Butler in A Jet Boat",
        "rating" : "PG-13",
        "price" : 100
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58563d"),
        "film_id" : 8,
        "title" : "AIRPORT POLLOCK",
        "description" : "A Epic Tale of a Moose And a Girl who must Confront a Monkey in Ancient India",
        "rating" : "R",
        "price" : 100
}
{
        "_id" : ObjectId("61d43f4b16f8f1b21f58563e"),
        "film_id" : 9,
        "title" : "ALABAMA DEVIL",
        "description" : "A Thoughtful Panorama of a Database Administrator And a Mad Scientist who must Outgun a Mad Scientist in A Jet Boat",
        "rating" : "PG-13",
        "price" : 100
}
```
Поле ```"price"``` со значением 100 добавлено к каждому объекту  

### Агрегация

Агрегация – это группировка значений многих документов.   
Операции агрегирования позволяют манипулировать такими сгруппированными данными (например, подсчёт – COUNT(*)).  
В MySQL аналогом агрегации является команда group by.

#### Метод aggregate()

В MongoDB для агрегации используется метод aggregate(). Данный метод имеет следующий синтаксис:  
>db.ИМЯ_КОЛЛЕКЦИИ.aggregate(ОПЕРАЦИЯ_АГРЕГАЦИИ)

#### Пример 1
Найдём суммарную стоимость фильмов с рейтингом "G"
>db.films.aggregate([{ $match: { rating: "G" } },{$group:{_id:"Total_price_G_rating","total_price":{$sum:"$price"}}}])  

#### Комментарий

+ $match  
Операция фильтрации, которая может уменьшить количество документов, которые передаются для ввода в следующий этап  
+ $group  
Непосредственно, сама агрегация  

Результат:
```json
{ 
    "_id" : "Total_price_G_rating", 
    "total_price" : 17800 
}
```

#### Пример 2
Найдём среднюю стоимость фильма с рейтингом G (она, естественно заранее известна и равна 100, но нас интересует сам механизм)  
> db.films.aggregate([{ $match: { rating: "G" } },{$group:{_id:"Average_price_G_rating","average_price":{$avg:"$price"}}}])

Результат:
```json
{ 
    "_id" : "Average_price_G_rating", 
    "average_price" : 100 
}
```

#### Пример 3
Найдём максимальную стоимость фильма с рейтингом G (то же самое)  
> db.films.aggregate([{ $match: { rating: "G" } },{$group:{_id:"Max_price_G_rating","max_price":{$max:"$price"}}}])

Результат:
```json
{ 
    "_id" : "Max_price_G_rating", 
    "max_price" : 100 
}
```

#### Пример 4
Найдем суммарную стоимость фильмов по рейтингам
> db.films.aggregate([{$group:{_id:"$rating","total_price":{$sum:"$price"}}}])

Результат:
```json
{ "_id" : "PG", "total_price" : 19400 }
{ "_id" : "G", "total_price" : 17800 }
{ "_id" : "R", "total_price" : 19500 }
{ "_id" : "PG-13", "total_price" : 22300 }
{ "_id" : "NC-17", "total_price" : 21000 }
```

#### Пример 5
Создадим поле и заполним случайными значениями с помощью Python
```python
from pymongo import MongoClient
import pymysql
import sys
import random

#Step 1: Connect to MongoDB - Note: Change connection string as needed
client = MongoClient(port=27017)
db=client.sakila
try:
    con = pymysql.connect(host='localhost', #коннект к MySQL
                          user='root',
                          password='',
                          database='sakila')
    cursor = con.cursor()
    # Step 2: Create sample data
    cursor.execute('SELECT film_id,title,description,rating FROM film')#выполнение запроса
    rows = cursor.fetchall()#сохранение записей
    for row in rows:
        film = {
            'film_id': row[0],
            'title': row[1],
            'description': row[2],
            'rating': row[3]
        }
        # Step 3: Insert sakila.films object directly into MongoDB via insert_one
        result = db.films.insert_one(film)
        db.films.update_one({'film_id': row[0]}, {'$set': {'price': random.randint(0, 100)}})
        # Step 4: Print to the console the ObjectID of the new document
        print('Created film {0} with inserted id: {1}'.format(row[1], result.inserted_id))
    #Print all films (db.films.deleteMany({}) - remove all objects from collection)
    data = db.films.find()
    print(data)
    for row in data:
        print(row)


except pymysql.err.OperationalError as e:
    print("Error reading data from MySQL table", e)
    print("Error %d: %s" % (e.args[0], e.args[1]))
    sys.exit(1)
```
