from django.shortcuts import render
from django.shortcuts import render, redirect
from django.http import HttpResponse
from django.views import generic
from .models import Consignment
from .models import Producer
from .models import Product
from . import forms

# Create your views here.

#consignment
def add_consignment(request):
    form = forms.Consignment_Form(request.POST)
    if request.method == "POST" and form.is_valid():
        data = form.cleaned_data
        form = form.save()

class Consignment_page_View(generic.TemplateView):
    template_name = 'consignments.html'

    def get(self, request):
        all_consignments = Consignment.objects.all()
        add_form = forms.Consignment_Form
        context = {
            'all_consignments': all_consignments,
            'add_form': add_form,
        }
        return render(request, self.template_name, context)

    def post(self, request):
        add_consignment(request)
        return redirect('/consignment/')

#producer
def add_producer(request):
    form = forms.Producer_Form(request.POST)
    if request.method == "POST" and form.is_valid():
        data = form.cleaned_data
        form = form.save()

class Producer_page_View(generic.TemplateView):
    template_name = 'producers.html'

    def get(self, request):
        all_producers = Producer.objects.all() #выборка всех производителей из бд
        add_form = forms.Producer_Form
        context = {
            'all_producers': all_producers,
            'add_form': add_form,
        }
        return render(request, self.template_name, context)

    def post(self, request):
        add_producer(request)
        return redirect('/producer/')

#product
def add_item(request):
    form = forms.Product_Form(request.POST)
    if request.method == "POST" and form.is_valid():
        data = form.cleaned_data
        form = form.save()

class Product_page_View(generic.TemplateView):
    template_name = 'products.html'

    def get(self, request):
        all_goods = Product.objects.all() #выборка всех товаров из бд
        add_form = forms.Product_Form
        context = {
            'all_goods': all_goods,
            'add_form': add_form,
        }
        return render(request, self.template_name, context)

    def post(self, request):
        add_item(request)
        return redirect('/product/')