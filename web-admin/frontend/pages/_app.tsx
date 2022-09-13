import type { AppProps } from 'next/app'
import 'antd/dist/antd.css'
import '../styles/vars.css'
import '../styles/global.css'

function MyApp({ Component, pageProps }: AppProps) {
  return <Component {...pageProps} />
}

export default MyApp
