import scrapy
import requests
import re
from ke_36.items import Ke36Item
from lxml import etree
import os
class A36krSpider(scrapy.Spider):
    name = '36kr'
    #allowed_domains = ['36kr.com']
    start_urls = ['https://36kr.com/information/web_news/']

    def parse(self, response):
        div_list=response.xpath('//div[@class="information-flow-list"]/div')
        url='https://36kr.com/information/web_news/'

        page = requests.get(url=url).text
        if not os.path.exists('./图片'):
            os.mkdir('./图片')
        #print(div_list)
        for div in div_list:
            URL=[]
            #print(div)
            title=div.xpath('.//div[2]/div[2]/p/a/text()')[0].extract()
            time=div.xpath('.//div[2]/div[2]/div[1]/span[2]/text()')[0].extract()
            author=div.xpath('.//div[2]/div[2]/div[1]/a/text()')[0].extract()
            #picture=div.xpath('.//div[2]/div/div[2]/div[1]/a/img/@src').extract()
            #picture=div.xpath('./div[@class="article-item-pic-wrapper"]/a[2]/img/@src').extract()
            urls=div.xpath('.//div[2]/div/div[2]/div[1]/a[2]/@href')[0].extract()

            ex='<div class="article-item-pic-wrapper">.*?<img src="(.*?)" alt.*?</div>'
            picture = re.findall(ex,page)
            #print(picture)
            for src in picture:
                Src = 'https:' + src
                img_data = requests.get(url=Src).content
                img_name = src.split('/')[-1]
                imgPath = './图片/' + img_name
                with open(imgPath, 'wb') as fp:
                    fp.write(img_data)
                    #print(img_name, '下载成功')

            URL.append(urls)
            # 将item提交到管道
            item = Ke36Item()
            item['title'] = title
            item['time'] = time
            item['author'] = author
            item['picture'] = picture
            #yield item
            #print(title,time,author)
            for url in URL:
                #print(url)
                new_url = 'https://36kr.com' + url
                page_text = scrapy.Request(url=new_url, meta={'item':item},callback=self.parse_deal)
                yield page_text

    def parse_deal(self, response):
        list=response.xpath('//div[@class="common-width margin-bottom-20"]/div')
        for list1 in list:
            CON=list1.xpath('.//p/text()').extract()
            content=''.join(CON)
            #print(CON)

        #item = Ke36Item()
        item=response.meta['item']
        item['content'] = content

        yield item

