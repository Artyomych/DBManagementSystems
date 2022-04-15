from django.conf.urls import url
from django.urls import path
from . import views

app_name = 'testApp'

urlpatterns = [
    path('consignment/',views.Consignment_page_View.as_view()),
    path('producer/',views.Producer_page_View.as_view()),
    path('product/',views.Product_page_View.as_view()),
]