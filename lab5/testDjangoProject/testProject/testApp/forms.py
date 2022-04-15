from django.forms import ModelForm
from django import forms
from .models import Consignment
from .models import Producer
from .models import Product

class Consignment_Form(ModelForm):
    class Meta:
        model = Consignment
        fields = ['product']

class Producer_Form(ModelForm):
    class Meta:
        model = Producer
        fields = ['name']

class Product_Form(ModelForm):
    class Meta:
        model = Product
        fields = ['name', 'price', 'firm']