function submitToIndexNow(node) {
  fetch(`/umbraco/backoffice/api/IndexNow/Submit?nodeId=${node.id}`,
    {
      method: 'post',
    })
    .then(response => response.json())
    .then(response => {
      if (response.success === true) {
        UmbSpeechBubble.ShowMessage('success', 'IndexNow', 'Submitted successfully');
      }
      else {
        console.log(response.errorMessage);
        UmbSpeechBubble.ShowMessage('error', 'IndexNow', response.errorMessage);
      }
    })
    .catch(err => {
      console.log(err);
      UmbSpeechBubble.ShowMessage('error', 'IndexNow', 'Submit failed');
    });
}