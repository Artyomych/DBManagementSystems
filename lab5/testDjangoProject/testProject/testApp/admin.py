from django.contrib import admin
from . import models
# Register your models here.

admin.site.register(models.Consignment)
admin.site.register(models.Producer)
admin.site.register(models.Product)