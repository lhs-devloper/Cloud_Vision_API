import json
from urllib import parse, request
api_key = f"{YOUR_API_KEY}" # Google Cloud Vision API_KEY
m_id = '/m/03c_qk' # Machine Learning ID
service_url = 'https://kgsearch.googleapis.com/v1/entities:search'
params = {
    'ids': m_id,
    'limit': 10,
    'indent': True,
    'languages': 'ko',
    'key': api_key,
}
url = service_url + '?' + parse.urlencode(params) # End url Parameter
# GET https://kgsearch.googleapis.com/v1/entities:search?ids={m_id}&limit=10&indent=true&languages=ko&key={YOUR_API_KEY}
# print(url) # Check Your URL
response = json.loads(request.urlopen(url).read()) # Response JSON

# JSON Source_Confirm
# for element in response['itemListElement']:
#   print(element['result']['name'] + ' (' + str(element['resultScore']) + ')')
