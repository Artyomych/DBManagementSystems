# Django Framework

### Создание первого Django-приложения
1) Установка pip
2) Установка виртаульного окружения 
> pip install virtualenv
3) Создать виртуальное окружение 
> python -m venv ENVIRONMENT_NAME
4) Активировать виртуальное окружение
> Путь: ENVIRONMENT_NAME/Scripts/activate - Windows
> source ENVIRONMENT_NAME/bin/activate - Linux

Пример активации среды (в скобках указано название среды):
> (venv) F:\django_projects\testDjangoProject>
6) Установить Django в виртуальное окружение (указать версию LTS https://www.djangoproject.com/download/)
> pip install django (установит последнюю официальную версию)

С указанием версии долгосрочной поддержки (LTS):
>pip install django==3.2
7) Создать проект
> django-admin startproject PROJECT_NAME
8) Запуск сервера Django
> python manage.py runserver   

Выше можно указать порт (порт по умолчанию 8000)
