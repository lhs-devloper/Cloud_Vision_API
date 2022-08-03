# 본 Repository에서는 Google Cloud Vision APi에 대해 작성하였습니다.
## Cloud Vision API Doucmentation
### Link: https://cloud.google.com/vision/docs/quickstarts
___
## 기본 요구사항
1. RESTFUL API지식(JSON형식으로 Request, Response를 할 수 있어야합니다.)
2. Google Cloud Vision설정에 대한 지식이 있어야합니다.
___
### 시작 전 Vision_API를 사용하기 위해 Request작성 이해
```json
{  
  "requests": [
    {
      "image": {
        "content": "string(image to Base64 code)",
        "source": {
            "gcsImageUri": "string(Google Cloud Storage URI)",
            "imageUri": "string(Access HTTP/HTTPs URI)"
        }
      },
      "features": [
        {
          "maxResults": "INT(as many results as you want)",
          "type": "Enum(Type) Reference Link: https://cloud.google.com/vision/docs/reference/rest/v1/Feature#Type",
          "model": "string(builtin/stable or builtin/latest)"
        }
      ],
      "imageContext": {
        "Update_Please": "lhs-devloper Confirm Please"
      }
    }
  ]
}
```
- 필수 JSON key값
1. image(content or source(gcsImageUri or ImageUri))
2. feature(type and maxResult)
- Request 값 이해(차후 업데이트 예정)
___
### 최종 KgSearch를 통해 얻은 API결과물
![API_RESULT](./images/API_Result.png)