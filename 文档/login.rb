#!/usr/bin/env ruby
require 'rest_client'
require 'timeout'

def http_request(method, uri, timeout, params)
  # 
  Timeout.timeout(timeout) do
    res = RestClient.send(method, uri, params)
    return res.body
  end
  rescue TimeoutError
    return nil
  rescue Exception => e
end

def http_post(uri, timeout, params)
  http_request 'post', uri, timeout, params
end
#服务器链接
url = "http://183.63.117.154:8010/v2/users/login"
time_out = 5
#提交服务器数据用户名密码
data = {"user"=>{"login"=>"user1", "password"=>"111111", "remember_me"=>"0"}}
#提交数据给服务器
respon_data = http_post(url, 5, data)


#打印返回数据
puts respon_data

#后面就跳到服务器

