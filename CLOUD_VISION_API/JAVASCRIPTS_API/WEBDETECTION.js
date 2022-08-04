// Imports the Google Cloud client library
// ES6 Moudule
// import vision from '@google-cloud/vision';
// ES6 previous
// const vision = require('@google-cloud/vision');

// Creates a client
const client = new vision.ImageAnnotatorClient();

const fileName = 'Local image file, e.g. /path/to/image.png';

// Detect similar images on the web to a local file
const [result] = await client.webDetection(fileName);
const webDetection = result.webDetection;
if (webDetection.fullMatchingImages.length) {
  console.log(
    `Full matches found: ${webDetection.fullMatchingImages.length}`
  );
  webDetection.fullMatchingImages.forEach(image => {
    console.log(`  URL: ${image.url}`);
    console.log(`  Score: ${image.score}`);
  });
}

if (webDetection.partialMatchingImages.length) {
  console.log(
    `Partial matches found: ${webDetection.partialMatchingImages.length}`
  );
  webDetection.partialMatchingImages.forEach(image => {
    console.log(`  URL: ${image.url}`);
    console.log(`  Score: ${image.score}`);
  });
}

if (webDetection.webEntities.length) {
  console.log(`Web entities found: ${webDetection.webEntities.length}`);
  webDetection.webEntities.forEach(webEntity => {
    console.log(`  Description: ${webEntity.description}`);
    console.log(`  Score: ${webEntity.score}`);
  });
}

if (webDetection.bestGuessLabels.length) {
  console.log(
    `Best guess labels found: ${webDetection.bestGuessLabels.length}`
  );
  webDetection.bestGuessLabels.forEach(label => {
    console.log(`  Label: ${label.label}`);
  });
}