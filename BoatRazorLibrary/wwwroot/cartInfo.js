export const userInfo = (userName) => alert(userName)

export const isDevice = () => {
    let deviceValue = /android|webos|iPhone|iPad|iPod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent)
    return deviceValue 
}
export const isDesktop = () => {
    //1575 inner windows
    //1424 inner desktop
    let defineDesktop = (window.innerWidth == 1424) ? true : false 
    return defineDesktop
}
const loadAsync = (url, callback) => {
    var s = document.createElement('script');
    s.setAttribute('src', url); s.onload = callback;
    document.head.insertBefore(s, document.head.firstElementChild);
}

export const payPalBtn = (baseOrderAmount) => {

    loadAsync('https://www.paypal.com/sdk/js?client-id=AWTeL4kMDevIsCS-YZzuwnpA2qET4Sb6zGapzyWN1py_CdjzNjFsBKmipq-0HdZqswRBgZO7MFr2gjcW&currency=USD', () => {

        paypal.Buttons({

            onShippingChange: (data, actions) => {
                if (data.shipping_address.country_code !== 'US') {
                  return  actions.reject();
                }
                const shippingAmount = data.shipping_address.state === 'CA' ? '0.00' : '5.90';
                return actions.order.patch([
                    {
                        op: 'replace',
                        path: '/purchase_units/@reference_id==\'default\'/amount',
                        value: {
                            currency_code: 'USD',
                            value: (parseFloat(baseOrderAmount) + parseFloat(shippingAmount)).toFixed(2),
                            breakdown: {
                                item_total: {
                                    currency_code: 'USD',
                                    value: baseOrderAmount
                                },
                                shipping: {
                                    currency_code: 'USD',
                                    value: shippingAmount
                                }
                            }
                        }
                    }
                ]);
            },

            onApprove: (data, actions) => {
                return actions.order.capture().then((details) => {
                    alert('Transaction completed by ' + details.payer.name.given_name);
                });
            },

            onCancel: (data) => {
                alert("order cancelled")
                return  window.location.href = "https://localhost:7016";
            }
        }).render('#paypal-purchase');
    })
}
