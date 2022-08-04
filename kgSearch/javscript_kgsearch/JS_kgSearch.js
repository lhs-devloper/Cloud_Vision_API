const GETINF = (req, res) => {
    const baseURL = 'https://kgsearch.googleapis.com/v1/entities:search'; 
    const API_KEY = `${YOUR_API_KEY}`; // Google Cloud Vision API_KEY
    const m_id = '/m/03c_qk' // Machine Learning ID
    const params = {
        'ids': m_id,
        'limit': 10,
        'indent': true,
        'languages': 'ko',
        'key': API_KEY,
    };
    const SearchParams = new URLSearchParams(params).toString();
    const final_url = `${baseURL}?${SearchParams}`;
    // console.log(final_url);
    const m_id_inf = await fetch(final_url, {
        method: "GET"
    })
    const reponse = m_id_inf.json();
    // console.log(reponse);
    return res.redirect(final_url);
}

