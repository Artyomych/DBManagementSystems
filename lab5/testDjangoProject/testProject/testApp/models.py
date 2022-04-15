from django.db import models

# Create your models here.

class Producer(models.Model):
    name = models.CharField(max_length=30, unique=True, verbose_name='Название')

    def __str__(self):
        return self.name

    class Meta:
        verbose_name = 'Производитель'
        verbose_name_plural = 'Производители'

class Product(models.Model):
    name = models.CharField(max_length=30, unique=False, verbose_name='Название')
    price = models.PositiveIntegerField(verbose_name = 'Цена')
    firm = models.ForeignKey(Producer, verbose_name='Производитель', on_delete=models.CASCADE)

    def __str__(self):
        return self.name

    class Meta:
        verbose_name = 'Продукт'
        verbose_name_plural = 'Продукты'

class Consignment(models.Model):
    product = models.ForeignKey(Product, verbose_name='Продукт',on_delete=models.CASCADE)

    def __str__(self):
        return '%s. Номер партии: %s' %(self.product, self.pk)

    class Meta:
        verbose_name = 'Партия'
        verbose_name_plural = 'Партии'