# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: https://docs.scrapy.org/en/latest/topics/item-pipeline.html


# useful for handling different item types with a single interface
from itemadapter import ItemAdapter

import pymysql

from pymongo import MongoClient

class Ke36Pipeline:
    fp=None
    #重写父类方法。该方法只会在爬虫开始时调用一次
    def open_spider(self,spider):
        print('开始爬取！')
        self.fp=open('./36ke.txt','w',encoding='utf-8')

    #每接收一个item就会调用一次
    def process_item(self, item, spider):
        author=item['author']
        title=item['title']
        time=item['time']
        content = item['content']
        self.fp.write(title+':'+author+':'+time+':'+content+'\n')
        #会传递给下一个即将执行的管道类
        return item

    #该方法只会在爬虫开始时调用一次
    def close_spider(self,spider):
        print('爬取结束！')
        self.fp.close()


class mysqlPileLine(object):
    conn=None
    cursor=None

    def open_spider(self,spider):
        self.conn=pymysql.Connect(host='127.0.0.1',port=3306,user='root',password='123456',db='36ke',charset='utf8')

    def process_item(self,item,spider):
        self.cursor=self.conn.cursor()
        print('开始爬取！')
        try:
            self.cursor.execute("insert into 36kr values('%s','%s','%s','%s')"%(item['title'],item['author'],item['time'],item['content']))
            self.conn.commit()
            print('爬取成功!')
        except Exception as e:
            print(e)
            self.conn.rollback()

        return item
    def close_spoder(self,spider):
        self.cursor.close()
        self.conn.close()


class MongoDBPileLine(object):
    def open_spider(self,spider):
        dbName='36ke'
        client = MongoClient('localhost',27017)  # 创建连接对象client
        db = client[dbName]  # 使用文档dbName='datago306'
        self.post = db['picture']

    def process_item(self,item,spider):
        #self.db.scrapy.insert(dict(item['picture']))
        job_info = dict(item)  # item转换为字典格式
        self.post.insert(job_info)
        #self.insert_db(item)
        return item

    def close_spoder(self, spider):
        self.db.close()
